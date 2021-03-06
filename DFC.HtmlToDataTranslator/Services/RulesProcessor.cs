﻿using DFC.HtmlToDataTranslator.Contracts;
using DFC.HtmlToDataTranslator.Rules;
using System.Collections.Generic;

namespace DFC.HtmlToDataTranslator.Services
{
    public class RulesProcessor
    {
        public string Process(string sourceValue)
        {
            var result = sourceValue;
            var rules = GetRules();
            foreach (var rule in rules)
            {
                result = rule.Process(result);
            }

            return result;
        }

        private IEnumerable<IPreProcessorRule> GetRules()
        {
            var rules = new List<IPreProcessorRule>();
            rules.Add(new DecodeHtmlRule());
            rules.Add(new ReplaceBrTagsWithPTagsRule());
            rules.Add(new ConvertLinksToTextRule());
            return rules;
        }
    }
}
