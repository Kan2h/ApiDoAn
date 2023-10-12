using Microsoft.AspNetCore.Mvc;

namespace BackendApp.Dtos.Common
{
    public class FilterDto
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        private string _keyword;
        public string Keyword
        {
            get => _keyword;
            set => _keyword = value?.Trim();
        }

        public int Skip()
        {
            return (PageIndex - 1) * PageSize;
        }
    }
}
