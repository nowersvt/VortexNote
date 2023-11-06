using VortexNote.Domain.Entities;
using VortexNote.Domain.Enums;

namespace VortexNote.Domain.Extentions
{
    public static class IQueryableNoteExtentions
    {
        public static IQueryable<Note> SortBy(this IQueryable<Note> query, NoteSortType sortType)
        {
            switch (sortType)
            {
                case NoteSortType.Creation:
                    return query.OrderBy(x=>x.CreatedAt);
                case NoteSortType.Updation:
                    return query;
                default:
                    return query;
            }
        }
    }
}
