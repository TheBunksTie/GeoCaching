using System;

namespace Swk5.GeoCaching.DomainModel {
    public enum FilterCriterium {
        Size,
        TerrainDifficulty,
        CacheDifficulty
    }

    public enum FilterOperation {
        Exact,
        Above,
        AboveEquals,
        Below,
        BelowEquals
    }

    public static class EnumExtension {
        public static string UiCaption(this FilterCriterium c) {
            if (c == FilterCriterium.Size) {
                return "by size";
            }
            if (c == FilterCriterium.CacheDifficulty) {
                return "by cache difficulty";
            }
            if (c == FilterCriterium.TerrainDifficulty) {
                return "by terrain difficulty";
            }

            throw new ArgumentException();
        }

        public static string UiCaption(this FilterOperation c) {
            if (c == FilterOperation.Below) {
                return "less than";
            }
            if (c == FilterOperation.BelowEquals) {
                return "less or equal than";
            }
            if (c == FilterOperation.Above) {
                return "greater than";
            }
            if (c == FilterOperation.AboveEquals) {
                return "greater or equal than";
            }
            if (c == FilterOperation.Exact) {
                return "equals";
            }
            throw new ArgumentException();
        }
    }
}