using System;
using System.Data;
using Swk5.GeoCaching.DomainModel;

namespace Swk5.GeoCaching.DAL.MySQLServer {
    public static class CacheFilterTransformer {
        
        public static string ToColumnName(this FilterCriterium c) {
            if (c == FilterCriterium.Size) {
                return "sizeCode";
            }
            if (c == FilterCriterium.TerrainDifficulty) {
                return "difficultyTerrain";
            }
            if (c == FilterCriterium.CacheDifficulty) {
                return "difficultyCache";
            }
            throw new ArgumentException();
        }

        public static DbType ToDataType(this FilterCriterium c) {
            if (c == FilterCriterium.Size) {
                return DbType.Int32;
            }
            if (c == FilterCriterium.TerrainDifficulty) {
                return DbType.Double;
            }
            if (c == FilterCriterium.CacheDifficulty) {
                return DbType.Double;
            }
            throw new ArgumentException();
        }

        public static string ToOperationName(this FilterOperation c) {
            if (c == FilterOperation.Below) {
                return "<";
            }
            if (c == FilterOperation.BelowEquals) {
                return "<=";
            }
            if (c == FilterOperation.Above) {
                return ">";
            }
            if (c == FilterOperation.AboveEquals) {
                return ">=";
            }
            if (c == FilterOperation.Exact) {
                return "=";
            }
            throw new ArgumentException();
        }
    }
}