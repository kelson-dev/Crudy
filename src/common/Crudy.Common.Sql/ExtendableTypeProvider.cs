using Crudy.Common.Sql.TypeResolvers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Crudy.Common.Sql
{
    public class ExtendableTypeProvider : IExtendableTypeResolver
    {
        private readonly Func<IStorage> database;

        private readonly Dictionary<Type, TypeHandler> _baseHandlers = new Dictionary<Type, TypeHandler>();

        private readonly Dictionary<Type, TypeHandler> _providedHandlers = new Dictionary<Type, TypeHandler>();

        private void RegisterBaseHandler<TClr, TDb>(TypeHandler<TClr, TDb> handler) => _baseHandlers[typeof(TClr)] = handler;

        protected void RegisterHandler<TClr, TDb>(TypeHandler<TClr, TDb> handler) => _providedHandlers[typeof(TClr)] = handler;

        protected TypeHandler GetHandler(Type type) =>
            _providedHandlers.ContainsKey(type) ? _providedHandlers[type] : _baseHandlers[type];

        public ExtendableTypeProvider(Func<IStorage> db)
        {
            this.database = db;
            RegisterBaseHandler(new BooleanHandler());
            RegisterBaseHandler(new Int8Handler());
            RegisterBaseHandler(new UInt8Handler());
            RegisterBaseHandler(new Int16Handler());
            RegisterBaseHandler(new UInt16Handler());
            RegisterBaseHandler(new Int32Handler());
            RegisterBaseHandler(new UInt32Handler());
            RegisterBaseHandler(new Int64Handler());
            RegisterBaseHandler(new UInt64Handler());
            RegisterBaseHandler(new FloatHandler());
            RegisterBaseHandler(new DoubleHandler());
            RegisterBaseHandler(new DecimalHandler());
            RegisterBaseHandler(new StringHandler());
            RegisterBaseHandler(new GuidHandler());
            RegisterBaseHandler(new DateTimeHandler());
            RegisterBaseHandler(new DateTimeOffsetHandler());
            RegisterBaseHandler(new ByteArrayHandler());
        }

        public object? DbDeserialize(Type type, object value) /*=> GetHandler(type).DeserializeObject(value);*/
        {
            if (value == DBNull.Value)
                return null;
            if (type.IsEnum)
                return Enum.ToObject(type, value);
            //if (IsOne(type))
            //    return Activator.CreateInstance(
            //        typeof(One<,>).MakeGenericType(type.GenericTypeArguments), value, database);
            //else if (IsOptional(type))
            //    return Activator.CreateInstance(
            //        typeof(Optional<,>).MakeGenericType(type.GenericTypeArguments), value, database);
            else if (IsNullable(type, out Type generic))
            {
                try { return GetHandler(generic).DeserializeObject(value); }
                catch (Exception) { return default; }
            }
            else
                return GetHandler(type).DeserializeObject(value);
        }

        public object DbSerialize(Type type, object value) /*=> GetHandler(type).SerializeObject(value);*/
        {
            if (value == null)
                return DBNull.Value;
            else if (value.GetType().IsEnum)
                return (int)value;
            //else if (value is IdConvertable<int> valId)
            //    return valId.ToId();
            //else if (value is IdConvertable<int?> refId)
            //    return refId.ToId();
            else if (IsNullable(type, out Type generic))
                return GetHandler(generic).SerializeObject(value);
            else
                return GetHandler(type).SerializeObject(value);
        }

        public DbType GetDbType(Column column) => GetHandler(UnwrapColumnType(column)).GetDbType(column);

        public string GetDbTypeString(Column column) => GetHandler(UnwrapColumnType(column)).GetDbTypeString(column);

        private Type UnwrapColumnType(Column column)
        {
            if (HasMapping(column.Type))
                return column.Type;
            else if (IsNullable(column.Type, out Type generic) && HasMapping(generic))
                return generic;
            else if (IsOne(column.Type) || IsOptional(column.Type))
                return typeof(int);
            else if (column.Type.IsEnum)
                return column.Type.GetEnumUnderlyingType();
            else
                throw new InvalidOperationException($"Cannot translate clr type {column.Type} to a database type");
        }

        private bool IsNullable(Type clrType, out Type generic)
            => (clrType.IsGenericType && clrType.GetGenericTypeDefinition() == typeof(Nullable<>)) ? (generic = clrType.GenericTypeArguments.FirstOrDefault()) != null : (generic = null) != null;

        private bool IsOne(Type clrType)
            => clrType.IsGenericType && clrType.GetGenericTypeDefinition() == typeof(One<,>);

        private bool IsOptional(Type clrType)
            => clrType.IsGenericType && clrType.GetGenericTypeDefinition() == typeof(Optional<,>);

        public bool IsReference(Type clrType) => IsOne(clrType) || IsOptional(clrType);

        public bool IsOneueType(Type clrType)
            => (clrType.IsGenericType
                && (clrType == typeof(One<,>).MakeGenericType(clrType.GenericTypeArguments)
                || clrType == typeof(Optional<,>).MakeGenericType(clrType.GenericTypeArguments)))
            || HasMapping(clrType);

        private bool HasMapping(Type clrType)
            => _baseHandlers.ContainsKey(clrType) || _providedHandlers.ContainsKey(clrType);
    }
}
