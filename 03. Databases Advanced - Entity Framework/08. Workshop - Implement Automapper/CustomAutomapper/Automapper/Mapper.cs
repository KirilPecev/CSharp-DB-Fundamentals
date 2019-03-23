namespace Automapper
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Autoapper;

    public class Mapper
    {
        private object MapObject(object source, object destination)
        {
            IEnumerable<PropertyInfo> destinationProperties = destination
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite);

            foreach (var destProp in destinationProperties)
            {
                PropertyInfo sourceProperty = source
                    .GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(p => p.Name == destProp.Name);

                if (sourceProperty != null)
                {
                    object sourceValue = sourceProperty.GetMethod.Invoke(source, new object[0]);
                    if (sourceValue == null)
                    {
                        continue;
                        throw new ArgumentException(ExceptionUtils.NullableSourceValueGetMethod);
                    }

                    if (ReflectionUtils.IsPrimitive(sourceValue.GetType()))
                    {
                        destProp.SetValue(destination, sourceProperty.GetValue(source, null), null);
                        continue;
                    }

                    if (ReflectionUtils.IsGenericCollection(sourceValue.GetType()))
                    {
                        if (ReflectionUtils.IsPrimitive(sourceValue.GetType().GetGenericArguments()[0]))
                        {
                            object destinationCollection = sourceValue;
                            destProp.SetMethod.Invoke(destination, new[] { destinationCollection });
                        }
                        else
                        {
                            object destinationCollection = destProp.GetMethod.Invoke(destination, new object[0]);
                            Type destinationType = destinationCollection.GetType().GetGenericArguments()[0];

                            foreach (var destP in (IEnumerable)sourceValue)
                            {
                                ((IList)destinationCollection).Add(this.CreateMappedObject(destP, destinationType));
                            }
                        }
                    }
                    else if (ReflectionUtils.IsNonGenericCollection(sourceValue.GetType()))
                    {
                        IList destinationCollection = (IList)Activator.CreateInstance(destProp.PropertyType,
                            new object[] { ((object[])sourceValue).Length });

                        for (int i = 0; i < ((object[])sourceValue).Length; i++)
                        {
                            destinationCollection[i] = this.CreateMappedObject(((object[])sourceValue)[i],
                                destProp.PropertyType.GetElementType());
                        }

                        destProp.SetValue(destination, destinationCollection);
                    }
                    else
                    {
                        destProp.SetValue(destination, this.CreateMappedObject(sourceProperty.GetValue(sourceValue), destProp.PropertyType));
                    }
                }
            }

            return destination;
        }

        private object CreateMappedObject(object source, Type returnType)
        {
            if (source == null || returnType == null)
            {
                throw new ArgumentException(ExceptionUtils.NullableSourceValueGetMethod);
            }

            object destination = Activator.CreateInstance(returnType);

            return MapObject(source, destination);
        }

        public TDest CreateMappedObject<TDest>(object source)
        {
            if (source == null)
            {
                throw new ArgumentException(ExceptionUtils.NullableSource);
            }

            object destination = Activator.CreateInstance(typeof(TDest));

            return (TDest)MapObject(source, destination);
        }
    }
}
