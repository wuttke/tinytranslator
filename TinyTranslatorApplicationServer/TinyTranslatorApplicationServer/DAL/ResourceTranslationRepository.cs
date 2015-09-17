﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TinyTranslatorApplicationServer.Model;

namespace TinyTranslatorApplicationServer.DAL
{
    public class ResourceTranslationRepository
    {
        private TinyTranslatorDbContext context;

        public ResourceTranslationRepository(TinyTranslatorDbContext context) 
        {
            this.context = context;
        }

        public void DeleteTranslationsForResource(Resource resource)
        {
            context.ResourceTranslations.RemoveRange(
                from translation in context.ResourceTranslations 
                where translation.ResourceID == resource.ID select translation
                );
        }
    }
}