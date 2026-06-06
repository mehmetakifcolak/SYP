import { BooleanEditor, initFormType, PasswordEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface LoginForm {
    Username: StringEditor;
    Password: PasswordEditor;
    RememberMe: BooleanEditor;
}

export class LoginForm extends PrefixedContext {
    static readonly formKey = 'Membership.Login';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!LoginForm.init) {
            LoginForm.init = true;

            var w0 = StringEditor;
            var w1 = PasswordEditor;
            var w2 = BooleanEditor;

            initFormType(LoginForm, [
                'Username', w0,
                'Password', w1,
                'RememberMe', w2
            ]);
        }
    }
}