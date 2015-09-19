using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TinyTranslatorApplicationServer.Model
{
    public enum TranslationStatus
    {
        // an Lingobit angleichen?
        // konfigurierbar?

        NOT_TRANSLATED = 0, // Nicht übersetzt
        NEW = 10, // Neu
        CORRECT = 20, // Zur Überarbeitung
        IMPORTED = 30, // Importiert
        APPROVED = 70, // Freigegeben
        FINAL = 100, // Endgültig

        NO_NEED_TO_TRANSLATE = -1 // Muss nicht übersetzt werden

    }
}