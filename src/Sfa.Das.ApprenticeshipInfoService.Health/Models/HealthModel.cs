namespace Sfa.Das.ApprenticeshipInfoService.Health.Models
{
    using System.Collections.Generic;

    public class HealthModel
    {
        public Status FEChoices { get; set; }
        public Status Status { get; set; }

        public List<string> Errors { get; set; }

        public IEnumerable<ElasticsearchAlias> ElasticSearchAliases { get; set; }

        public Status CoreAliasesExistStatus { get; set; }

        public ElasticsearchLog ElasticsearchLog { get; set; }

        public Status LarsZipFileStatus { get; set; }

        public Status LarsFilePageStatus { get; set; }

        public string LarsFileDateStamp { get; set; }

        public Status CourseDirectoryStatus { get; set; }

        public long Took { get; set; }

        public Status UkrlpStatus { get; set; }
    }
}
