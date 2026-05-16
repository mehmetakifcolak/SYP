import { ErrorHandling, Widget, WidgetProps, getReturnUrl, notifyError, resolveUrl, serviceCall } from "@serenity-is/corelib";
import { LoginRequest } from "../../../ServerTypes/Membership";
import { LoginFormTexts } from "../../../ServerTypes/Texts";

interface Particle {
    x: number;
    y: number;
    size: number;
    speedX: number;
    speedY: number;
    opacity: number;
    pulse: number;
}

export default function pageInit(opt?: { activated: string }) {
    var loginPanel = new LoginPanel({ element: '#LoginPanel' });

    if (opt?.activated) {
        loginPanel.setUsername(opt.activated);
        loginPanel.focusPassword();
    }
}

class LoginPanel extends Widget<any> {

    private usernameInput: HTMLInputElement;
    private passwordInput: HTMLInputElement;
    private carouselIndex: number = 0;
    private carouselInterval: number | null = null;
    private particleCanvas: HTMLCanvasElement | null = null;
    private particleCtx: CanvasRenderingContext2D | null = null;
    private particles: Particle[] = [];
    private animationFrameId: number | null = null;

    constructor(props?: WidgetProps<any>) {
        super(props);
    }

    private initCarousel() {
        const carousel = document.getElementById('loginCarousel');
        if (!carousel) return;

        const dots = document.querySelectorAll('.login-carousel-dot');
        const cards = document.querySelectorAll('.login-carousel-card');

        if (cards.length === 0) return;

        const totalCards = cards.length;

        const updateCarousel = (index: number) => {
            this.carouselIndex = index;

            cards.forEach((card, i) => {
                const el = card as HTMLElement;
                const offset = i - index;

                el.classList.remove('active', 'prev', 'next', 'far-prev', 'far-next');

                if (offset === 0) {
                    el.classList.add('active');
                } else if (offset === 1 || (index === totalCards - 1 && i === 0)) {
                    el.classList.add('next');
                } else if (offset === -1 || (index === 0 && i === totalCards - 1)) {
                    el.classList.add('prev');
                } else if (offset > 1) {
                    el.classList.add('far-next');
                } else {
                    el.classList.add('far-prev');
                }
            });

            dots.forEach((dot, i) => {
                dot.classList.toggle('active', i === index);
            });
        };

        // İlk durumu ayarla
        updateCarousel(0);

        // Otomatik geçiş
        this.carouselInterval = window.setInterval(() => {
            const nextIndex = (this.carouselIndex + 1) % totalCards;
            updateCarousel(nextIndex);
        }, 3000);

        // Dot tıklama
        dots.forEach((dot) => {
            dot.addEventListener('click', (e: Event) => {
                const target = e.currentTarget as HTMLElement;
                const index = parseInt(target.dataset.index || '0', 10);
                updateCarousel(index);

                // Otomatik geçişi sıfırla
                if (this.carouselInterval) {
                    clearInterval(this.carouselInterval);
                    this.carouselInterval = window.setInterval(() => {
                        const nextIndex = (this.carouselIndex + 1) % totalCards;
                        updateCarousel(nextIndex);
                    }, 3000);
                }
            });
        });
    }

    public setUsername(value: string) {
        if (this.usernameInput) {
            this.usernameInput.value = value;
        }
    }

    public focusPassword() {
        if (this.passwordInput) {
            this.passwordInput.focus();
        }
    }

    protected getLoginRequest(): LoginRequest {
        return {
            Username: this.usernameInput?.value || '',
            Password: this.passwordInput?.value || ''
        };
    }

    protected validateForm(): boolean {
        const usernameWrapper = this.usernameInput?.closest('.login-input-wrapper');
        const passwordWrapper = this.passwordInput?.closest('.login-input-wrapper');

        // Reset states
        usernameWrapper?.classList.remove('error');
        passwordWrapper?.classList.remove('error');

        let isValid = true;

        if (!this.usernameInput?.value?.trim()) {
            usernameWrapper?.classList.add('error');
            this.usernameInput?.focus();
            isValid = false;
        } else if (!this.passwordInput?.value?.trim()) {
            passwordWrapper?.classList.add('error');
            this.passwordInput?.focus();
            isValid = false;
        }

        return isValid;
    }

    protected loginClick() {
        if (!this.validateForm())
            return;

        var request = this.getLoginRequest();

        serviceCall({
            url: resolveUrl('~/Account/Login'),
            request: request,
            onSuccess: () => {
                this.redirectToReturnUrl();
            },
            onError: response => {

                if (response?.Error?.Code === "RedirectUserTo") {
                    window.location.href = response.Error.Arguments;
                    return;
                }

                if (response?.Error?.Message?.length) {
                    notifyError(response.Error.Message);
                    this.passwordInput?.focus();
                    return;
                }

                ErrorHandling.showServiceError(response.Error);
            }
        });
    }

    protected redirectToReturnUrl() {
        window.location.href = getReturnUrl({ purpose: "login" });
    }

    protected override renderContents() {
        const id = this.useIdPrefix();

        this.element.empty().append(<>
            {/* Sol Panel - Marka Alanı */}
            <div class="login-brand-panel">
                {/* Partikül Canvas */}
                <canvas id="particleCanvas" class="login-particles-canvas"></canvas>
                {/* Arka plan animasyonlu grid */}
                <div class="login-bg-grid"></div>
                <div class="login-bg-glow"></div>

                <div class="login-brand-content">
                    <h1 class="login-brand-title">Sipariş Yönetim Portalı</h1>
                    <p class="login-brand-tagline">Liman - Siparişlerinizi tek noktadan yönetin</p>

                    {/* 3D Carousel */}
                    <div class="login-carousel-wrapper">
                        <div class="login-carousel" id="loginCarousel">
                            <div class="login-carousel-card" data-index="0">
                                <div class="login-card-icon">
                                    <i class="fa fa-store"></i>
                                </div>
                                <h3>Bayi Yönetimi</h3>
                                <p>Bayilerinizi tek merkezden kolayca yönetin</p>
                            </div>
                            <div class="login-carousel-card" data-index="1">
                                <div class="login-card-icon">
                                    <i class="fa fa-box"></i>
                                </div>
                                <h3>Stok Yönetimi</h3>
                                <p>Anlık stok takibi ve otomatik uyarılar</p>
                            </div>
                            <div class="login-carousel-card" data-index="2">
                                <div class="login-card-icon">
                                    <i class="fa fa-chart-line"></i>
                                </div>
                                <h3>Detaylı Raporlar</h3>
                                <p>Satış analizleri ve performans metrikleri</p>
                            </div>
                            <div class="login-carousel-card" data-index="3">
                                <div class="login-card-icon">
                                    <i class="fa fa-clipboard-list"></i>
                                </div>
                                <h3>Kolay Sipariş Yönetimi</h3>
                                <p>Siparişlerinizi tek ekrandan kolayca yönetin</p>
                            </div>
                            <div class="login-carousel-card" data-index="4">
                                <div class="login-card-icon">
                                    <i class="fa fa-search-location"></i>
                                </div>
                                <h3>Kolay Takip</h3>
                                <p>Tüm süreçlerinizi anlık olarak takip edin</p>
                            </div>
                        </div>
                        {/* Carousel Dots */}
                        <div class="login-carousel-dots">
                            <span class="login-carousel-dot active" data-index="0"></span>
                            <span class="login-carousel-dot" data-index="1"></span>
                            <span class="login-carousel-dot" data-index="2"></span>
                            <span class="login-carousel-dot" data-index="3"></span>
                            <span class="login-carousel-dot" data-index="4"></span>
                        </div>
                    </div>
                </div>

                <div class="login-brand-footer">
                    © {new Date().getFullYear()} Liman - Sipariş Yönetim Portalı
                </div>
            </div>

            {/* Sağ Panel - Login Formu */}
            <div class="login-form-panel">
                <div class="login-form-container">
                    <div class="login-form-header">
                        <h2 class="login-form-title">Hoş Geldiniz</h2>
                        <p class="login-form-subtitle">Hesabınıza giriş yapın</p>
                    </div>

                    <form id={id.Form} action="" class="login-form">
                        {/* Kullanıcı Adı */}
                        <div class="login-input-group">
                            <label class="login-input-label">Kullanıcı Adı</label>
                            <div class="login-input-wrapper">
                                <i class="fa fa-user login-input-icon"></i>
                                <input
                                    type="text"
                                    id={id.Username}
                                    name="Username"
                                    class="login-input"
                                    placeholder="Kullanıcı adınızı girin"
                                    autoComplete="username"
                                    ref={el => this.usernameInput = el as HTMLInputElement}
                                />
                            </div>
                        </div>

                        {/* Şifre */}
                        <div class="login-input-group">
                            <label class="login-input-label">Şifre</label>
                            <div class="login-input-wrapper">
                                <i class="fa fa-lock login-input-icon"></i>
                                <input
                                    type="password"
                                    id={id.Password}
                                    name="Password"
                                    class="login-input"
                                    placeholder="Şifrenizi girin"
                                    autoComplete="current-password"
                                    ref={el => this.passwordInput = el as HTMLInputElement}
                                />
                                <button type="button" class="login-password-toggle" tabIndex={-1}
                                    onClick={e => this.togglePasswordVisibility(e)}>
                                    <i class="fa fa-eye"></i>
                                </button>
                            </div>
                        </div>

                        <div class="login-options">
                            <label class="login-remember">
                                <input type="checkbox" class="form-check-input" />
                                <span>Beni hatırla</span>
                            </label>
                            <a class="login-forgot" href={resolveUrl('~/Account/ForgotPassword')}>
                                {LoginFormTexts.ForgotPassword}
                            </a>
                        </div>

                        <button id={id.LoginButton} type="submit" class="btn btn-primary login-submit-btn"
                            onClick={e => {
                                e.preventDefault();
                                this.loginClick();
                            }}>
                            <i class="fa fa-sign-in-alt me-2"></i>
                            {LoginFormTexts.SignInButton}
                        </button>
                    </form>

                    <div class="login-signup">
                        Hesabınız yok mu?{' '}
                        <a href={resolveUrl('~/Account/SignUp')}>{LoginFormTexts.SignUpButton}</a>
                    </div>
                </div>
            </div>
        </>);

        // Carousel'i başlat
        setTimeout(() => this.initCarousel(), 100);

        // Partikülleri başlat
        setTimeout(() => this.initParticles(), 200);
    }

    private initParticles() {
        this.particleCanvas = document.getElementById('particleCanvas') as HTMLCanvasElement;
        if (!this.particleCanvas) return;

        this.particleCtx = this.particleCanvas.getContext('2d');
        if (!this.particleCtx) return;

        // Canvas boyutunu ayarla
        this.resizeCanvas();
        window.addEventListener('resize', () => this.resizeCanvas());

        // Partikülleri oluştur
        this.createParticles();

        // Animasyonu başlat
        this.animateParticles();
    }

    private resizeCanvas() {
        if (!this.particleCanvas) return;
        const brandPanel = document.querySelector('.login-brand-panel') as HTMLElement;
        if (brandPanel) {
            this.particleCanvas.width = brandPanel.offsetWidth;
            this.particleCanvas.height = brandPanel.offsetHeight;
        }
    }

    private createParticles() {
        if (!this.particleCanvas) return;

        const particleCount = Math.floor((this.particleCanvas.width * this.particleCanvas.height) / 8000);
        this.particles = [];

        for (let i = 0; i < particleCount; i++) {
            this.particles.push({
                x: Math.random() * this.particleCanvas.width,
                y: Math.random() * this.particleCanvas.height,
                size: Math.random() * 3 + 1,
                speedX: (Math.random() - 0.5) * 0.5,
                speedY: (Math.random() - 0.5) * 0.5,
                opacity: Math.random() * 0.5 + 0.2,
                pulse: Math.random() * Math.PI * 2
            });
        }
    }

    private animateParticles() {
        if (!this.particleCanvas || !this.particleCtx) return;

        this.particleCtx.clearRect(0, 0, this.particleCanvas.width, this.particleCanvas.height);

        // Partikülleri çiz ve güncelle
        this.particles.forEach((particle, index) => {
            // Pulse efekti
            particle.pulse += 0.02;
            const pulseOpacity = particle.opacity + Math.sin(particle.pulse) * 0.2;

            // Partikülü çiz
            this.particleCtx!.beginPath();
            this.particleCtx!.arc(particle.x, particle.y, particle.size, 0, Math.PI * 2);
            this.particleCtx!.fillStyle = `rgba(255, 255, 255, ${Math.max(0, Math.min(1, pulseOpacity))})`;
            this.particleCtx!.fill();

            // Glow efekti
            this.particleCtx!.beginPath();
            this.particleCtx!.arc(particle.x, particle.y, particle.size * 2, 0, Math.PI * 2);
            const gradient = this.particleCtx!.createRadialGradient(
                particle.x, particle.y, 0,
                particle.x, particle.y, particle.size * 2
            );
            gradient.addColorStop(0, `rgba(255, 255, 255, ${pulseOpacity * 0.3})`);
            gradient.addColorStop(1, 'rgba(255, 255, 255, 0)');
            this.particleCtx!.fillStyle = gradient;
            this.particleCtx!.fill();

            // Pozisyonu güncelle
            particle.x += particle.speedX;
            particle.y += particle.speedY;

            // Sınırları kontrol et
            if (particle.x < 0) particle.x = this.particleCanvas!.width;
            if (particle.x > this.particleCanvas!.width) particle.x = 0;
            if (particle.y < 0) particle.y = this.particleCanvas!.height;
            if (particle.y > this.particleCanvas!.height) particle.y = 0;

            // Yakın partiküller arasında çizgi çiz
            this.particles.slice(index + 1).forEach(otherParticle => {
                const dx = particle.x - otherParticle.x;
                const dy = particle.y - otherParticle.y;
                const distance = Math.sqrt(dx * dx + dy * dy);

                if (distance < 120) {
                    this.particleCtx!.beginPath();
                    this.particleCtx!.moveTo(particle.x, particle.y);
                    this.particleCtx!.lineTo(otherParticle.x, otherParticle.y);
                    this.particleCtx!.strokeStyle = `rgba(255, 255, 255, ${0.15 * (1 - distance / 120)})`;
                    this.particleCtx!.lineWidth = 0.5;
                    this.particleCtx!.stroke();
                }
            });
        });

        this.animationFrameId = requestAnimationFrame(() => this.animateParticles());
    }

    private togglePasswordVisibility(e: Event) {
        const button = e.currentTarget as HTMLButtonElement;
        const icon = button.querySelector('i');

        if (this.passwordInput.type === 'password') {
            this.passwordInput.type = 'text';
            icon?.classList.remove('fa-eye');
            icon?.classList.add('fa-eye-slash');
        } else {
            this.passwordInput.type = 'password';
            icon?.classList.remove('fa-eye-slash');
            icon?.classList.add('fa-eye');
        }
    }

    private demoLogin() {
        serviceCall({
            url: resolveUrl('~/Account/Login'),
            request: {
                Username: 'admin',
                Password: 'admin'
            } as LoginRequest,
            onSuccess: () => {
                this.redirectToReturnUrl();
            },
            onError: response => {
                if (response?.Error?.Code === "RedirectUserTo") {
                    window.location.href = response.Error.Arguments;
                    return;
                }

                if (response?.Error?.Message?.length) {
                    notifyError(response.Error.Message);
                    return;
                }

                ErrorHandling.showServiceError(response.Error);
            }
        });
    }
}
