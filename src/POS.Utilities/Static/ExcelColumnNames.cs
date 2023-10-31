﻿namespace POS.Utilities.Static
{
    public class ExcelColumnNames
    {
        public static List<TableColumn> GetColumns(IEnumerable<(string ColumnName, string PropertyName)> columnsProperties)
        {
            var columns = new List<TableColumn>();

            foreach (var (ColumnName, PropertyName) in columnsProperties)
            {
                var column = new TableColumn()
                {
                    Label = ColumnName,
                    PropertyName = PropertyName
                };

                columns.Add(column);
            }
            return columns;
        }

        #region ColumnCategories
        public static List<(string ColumnName, string PropertyName)> GetColumnsCategory()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
            {
                ("NOMBRE", "Name"),
                ("DESCRIPCIÓN", "Description"),
                ("FECHA DE CREACIÓN", "AuditCreateDate"),
                ("ESTADO", "StateCategory")
            };

            return columnsProperties;
        }
        #endregion

        #region ColumnProviders
        public static List<(string ColumnName, string PropertyName)> GetColumnsProviders()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
            {
                ("NOMBRE", "Name"),
                ("EMAIL", "Email"),
                ("TIPO DE DOCUMENTO", "DocumentNumber"),
                ("Nº DE DOCUMENTO", "DocumentNumber"),
                ("DIRECCIÓN", "Address"),
                ("TELÉFONO", "Phone"),
                ("FECHA DE CREACIÓN", "AuditCreateDate"),
                ("ESTADO", "StateProvider")
            };

            return columnsProperties;
        }
        #endregion

        #region ColumnDocumentTypes
        public static List<(string ColumnName, string PropertyName)> GetColumnsDocumentTypes()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
            {
                ("NOMBRE", "Name"),
                ("CODIGO", "Code"),
                ("ABREVIACIÓN", "Abbreviation"),
                ("FECHA DE CREACIÓN", "AuditCreateDate"),
                ("ESTADO", "StateDocumentType")
            };

            return columnsProperties;
        }
        #endregion

        #region ColumnClients
        public static List<(string ColumnName, string PropertyName)> GetColumnsClients()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
            {
                ("NOMBRE CLIENTE", "Name"),
                ("EMAIL", "Email"),
                ("TIPO DE DOCUMENTO", "DocumentNumber"),
                ("Nº DE DOCUMENTO", "DocumentType"),
                ("DIRECCIÓN", "Address"),
                ("TELÉFONO", "Phone"),
                ("FECHA DE CREACIÓN", "AuditCreateDate"),
                ("ESTADO", "StateProvider")
            };

            return columnsProperties;
        }
        #endregion

        #region ColumnProducts
        public static List<(string ColumnName, string PropertyName)> GetColumnsProduts()
        {
            var columnsProperties = new List<(string ColumnName, string PropertyName)>
            {
                ("NOMBRE PRODUCTO", "Name"),
                ("CATEGORIA", "Category"),
                ("PROVEEDOR", "provider"),
                ("CANTIDAD", "Stock"),
                ("PRECIO DE VENTA", "SellPrice"),
                ("FECHA DE CREACIÓN", "AuditCreateDate"),
                ("ESTADO", "StateProvider")
            };

            return columnsProperties;
        }
        #endregion


    }
}
