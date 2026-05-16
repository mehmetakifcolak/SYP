import { Config, DataGrid, ErrorHandling, TranslationConfig } from "@serenity-is/corelib";
import { UserPreferenceStorage } from "@serenity-is/extensions";
import { gridDefaults } from "@serenity-is/sleekgrid";
import flatpickr from "flatpickr";
import "flatpickr/dist/l10n";
import { getLanguageList } from "./Helpers/LanguageList";
import DOMPurify from "dompurify";

Config.rootNamespaces.push('SYP');
Config.rootNamespaces.push('_Ext');
DataGrid.defaultPersistanceStorage = new UserPreferenceStorage();
TranslationConfig.getLanguageList = getLanguageList;
gridDefaults.sanitizer = (globalThis.DOMPurify = DOMPurify).sanitize;

let culture = (document.documentElement?.lang || 'en').toLowerCase();
if (flatpickr.l10ns[culture]) {
    flatpickr.localize(flatpickr.l10ns[culture]);
} else {
    culture = culture.split('-')[0];
    flatpickr.l10ns[culture] && flatpickr.localize(flatpickr.l10ns[culture]);
}

window.onerror = ErrorHandling.runtimeErrorHandler;
window.addEventListener('unhandledrejection', ErrorHandling.unhandledRejectionHandler);
