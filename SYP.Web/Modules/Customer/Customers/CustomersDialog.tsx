import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators, EditorUtils, ToolButton, notifySuccess, serviceRequest } from '@serenity-is/corelib';
import { CustomersForm, CustomersRow, CustomersService } from '../../ServerTypes/Customer';

@Decorators.registerClass('SYP.Customer.CustomersDialog')
export class CustomersDialog extends DialogBase<CustomersRow, any> {
    protected getFormKey() { return CustomersForm.formKey; }
    protected getRowDefinition() { return CustomersRow; }
    protected getService() { return CustomersService.baseUrl; }

    protected form = new CustomersForm(this.idPrefix);

    constructor(opt?: any) {
        super(opt);

        // Password validation kuralları
        this.form.Password.addValidationRule(this.uniqueName, e => {
            if (this.form.Password.value && this.form.Password.value.length > 0 && this.form.Password.value.length < 6)
                return "Şifre en az 6 karakter olmalıdır!";
            return null;
        });

        this.form.PasswordConfirm.addValidationRule(this.uniqueName, e => {
            if (this.form.Password.value && this.form.Password.value != this.form.PasswordConfirm.value)
                return "Şifreler eşleşmiyor!";
            return null;
        });

        // Password değiştiğinde PasswordConfirm zorunluluğunu güncelle
        this.form.Password.change(() => {
            EditorUtils.setRequired(this.form.PasswordConfirm,
                this.form.Password.value && this.form.Password.value.length > 0);
        });
    }

    protected getToolbarButtons(): ToolButton[] {
        let buttons = super.getToolbarButtons();

        // Kullanıcı Oluştur butonu ekle
        buttons.push({
            title: 'Kullanıcı Oluştur',
            cssClass: 'create-user-button',
            icon: 'fa-user-plus text-blue',
            onClick: () => {
                this.createUserClick();
            }
        });

        return buttons;
    }

    protected updateInterface() {
        super.updateInterface();

        // Kullanıcı Oluştur butonunun görünürlüğünü ayarla
        const hasUser = this.entity?.UserId != null;
        const hasEmail = !!(this.entity?.Email);
        const isNew = this.isNewOrDeleted();

        // Buton: Sadece edit modunda, kullanıcı yoksa ve email varsa görünür
        this.toolbar.findButton('create-user-button')
            .toggleClass('disabled', isNew || hasUser || !hasEmail)
            .toggle(!isNew);

        // Password alanları: Yeni kayıtta veya kullanıcı yoksa görünür
        const showPasswordFields = isNew || !hasUser;
        this.form.Password.element.closest('.field').toggle(showPasswordFields);
        this.form.PasswordConfirm.element.closest('.field').toggle(showPasswordFields);

        // UserId ve Username alanları: Sadece kullanıcı varsa görünür
        if (this.form.UserId) {
            this.form.UserId.element.closest('.field').toggle(hasUser);
        }
        if (this.form.Username) {
            this.form.Username.element.closest('.field').toggle(hasUser);
        }
    }

    protected afterLoadEntity() {
        super.afterLoadEntity();

        // Password alanlarını temizle (güvenlik için)
        this.form.Password.value = '';
        this.form.PasswordConfirm.value = '';

        // Password alanları yeni kayıtta required değildir
        // (isteğe bağlı kullanıcı oluşturma)
        EditorUtils.setRequired(this.form.Password, false);
        EditorUtils.setRequired(this.form.PasswordConfirm, false);
    }

    private createUserClick() {
        // Validation
        if (!this.entity?.Id) {
            return;
        }

        if (this.entity?.UserId) {
            notifySuccess('Bu müşteri için zaten bir kullanıcı mevcut!');
            return;
        }

        if (!this.entity?.Email) {
            notifySuccess('Kullanıcı oluşturmak için email adresi zorunludur!');
            return;
        }

        // Şifre girişi için dialog göster
        this.showPasswordDialog();
    }

    private showPasswordDialog() {
        const password = prompt('Yeni kullanıcı için şifre girin (min 6 karakter):');
        if (!password) return;

        if (password.length < 6) {
            alert('Şifre en az 6 karakter olmalıdır!');
            return;
        }

        const passwordConfirm = prompt('Şifreyi tekrar girin:');
        if (password !== passwordConfirm) {
            alert('Şifreler eşleşmiyor!');
            return;
        }

        // CreateUser servisini çağır
        serviceRequest(CustomersService.baseUrl + '/CreateUser', {
            CustomerId: this.entity.Id,
            Password: password,
            PasswordConfirm: passwordConfirm
        }, (response: any) => {
            notifySuccess(`Kullanıcı "${response.Username}" başarıyla oluşturuldu!`);
            this.reloadById();
        });
    }
}