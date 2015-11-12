using System;
using System.Text;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Impl
{
    public static class SelectListBuilder
    {
        public static string BuildSelectListFor(
            DtoMetadata metadata,
            string tableAlias = null)
        {
            var buffer = new StringBuilder();
            AppendSelectListAndGetFirstColumnFor(buffer, metadata, tableAlias);
            return buffer.ToString();
        }

        public static string AppendSelectListAndGetFirstColumnFor(
            StringBuilder buffer,
            DtoMetadata metadata,
            string tableAlias = null)
        {
            return AppendSelectListAndGetFirstColumnFor(buffer, metadata, false, tableAlias);
        }

        public static string AppendSelectListAndGetFirstColumnFor(
            StringBuilder buffer,
            DtoMetadata metadata,
            bool iNeedALeadingComma,
            string tableAlias = null)
        {
            string firstColumn = null;
            foreach (var property in metadata.AllProperties)
            {
                //  At the moment this isn't sophisticated enough to drill down through tables.
                //  We might want to add this in future but, given it's currently only used to retrieve
                //  data to populate lists and dropdowns it seems unnecessary.
                if (!property.Prop.CanWrite
                    || property.HasAttribute<OneToManyAttribute>()
                    || property.HasAttribute<ManyToManyAttribute>()
                    || property.HasAttribute<ManyToOneAttribute>()
                    || property.HasAttribute<OneToOneAttribute>()
                    || property.HasAttribute<SimpleLoadIgnoreAttribute>()
                    || property.IsEnumerable)
                {
                    continue;
                }

                //if (property.HasAttribute<OneToOneAttribute>()
                //    && !string.IsNullOrEmpty(property.GetAttribute<OneToOneAttribute>().ChildForeignKeyColumn))
                //{
                //    continue;
                //}

                if (buffer.Length > 0 || iNeedALeadingComma)
                {
                    buffer.Append(", ");
                    buffer.Append(Environment.NewLine);
                    buffer.Append("    ");
                }

                if (!string.IsNullOrEmpty(tableAlias))
                {
                    buffer.Append('[');
                    buffer.Append(tableAlias);
                    buffer.Append("].");
                }

                buffer.Append('[');
                buffer.Append(property.ColumnName);
                buffer.Append(']');

                var columnAlias = property.ColumnName;

                if (columnAlias != property.Prop.Name
                    && ! property.HasAttribute<ManyToOneAttribute>()
                    && ! property.HasAttribute<OneToOneAttribute>())
                {
                    columnAlias = property.Prop.Name;
                    buffer.Append(" AS [");
                    buffer.Append(columnAlias);
                    buffer.Append("]");
                }

                if (firstColumn == null)
                {
                    firstColumn = columnAlias;
                }
            }
            return firstColumn;
        }
    }
}
