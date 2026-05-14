import { ErrorHandling, Widget, WidgetProps, getReturnUrl, notifyError, resolveUrl, serviceCall } from "@serenity-is/corelib";
import { LoginRequest } from "../../../ServerTypes/Membership";
import { LoginFormTexts } from "../../../ServerTypes/Texts";

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

    constructor(props?: WidgetProps<any>) {
        super(props);
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
                <div class="login-brand-content">
                    <div class="login-brand-logo">
                        <img class="s-site-logo-img" alt="Logo" />
                    </div>
                    <h1 class="login-brand-title">Sipariş Yönetim Portalı</h1>
                    <p class="login-brand-tagline">Siparişlerinizi tek noktadan yönetin</p>
                    <div class="login-brand-features">
                        <div class="login-feature-item">
                            <i class="fa fa-shopping-cart"></i>
                            <span>Sipariş Takibi</span>
                        </div>
                        <div class="login-feature-item">
                            <i class="fa fa-truck"></i>
                            <span>Sevkiyat Yönetimi</span>
                        </div>
                        <div class="login-feature-item">
                            <i class="fa fa-chart-bar"></i>
                            <span>Anlık Raporlar</span>
                        </div>
                    </div>
                </div>
                <div class="login-brand-footer">
                    © {new Date().getFullYear()} Sipariş Yönetim Portalı. Tüm hakları saklıdır.
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

                    <div class="login-divider">
                        <span>veya</span>
                    </div>

                    <div class="login-signup">
                        Hesabınız yok mu?{' '}
                        <a href={resolveUrl('~/Account/SignUp')}>{LoginFormTexts.SignUpButton}</a>
                    </div>
                </div>
            </div>
        </>);
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
}
