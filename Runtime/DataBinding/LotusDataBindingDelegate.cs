﻿//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема связывания данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusDataBindingDelegate.cs
*		Реализация класса для связывания данных на основе делегата.
*		Для связывания параметров используется технология Delegate.CreateDelegate что обеспечивает более быстрое 
*	обновление свойств и полей.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreDataBinding
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс реализующий привязку данных между свойством/методом объекта модели и объекта представления
		/// </summary>
		/// <remarks>
		/// Реализация класса для связывания данных.
		/// Для связывания параметров используется технология <see cref="Delegate.CreateDelegate(Type, Object, String)"/> 
		/// что обеспечивает более быстрое обновление свойств и полей
		/// </remarks>
		/// <typeparam name="TTypeModel">Тип члена объекта модели</typeparam>
		/// <typeparam name="TTypeView">Тип члена объекта представления</typeparam>
		//-------------------------------------------------------------------------------------------------------------
		[Serializable]
		public class BindingDelegate<TTypeModel, TTypeView> : CBindingBase
		{
			#region ======================================= ДАННЫЕ ====================================================
			// Основные параметры
			protected internal Action<TTypeModel> mActionModel;
			protected internal Action<TTypeView> mActionView;
			protected internal Func<TTypeView, TTypeModel> mOnConvertToModel;
			protected internal Func<TTypeModel, TTypeView> mOnConvertToView;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Делегат для установки значений объекту модели
			/// </summary>
			public Action<TTypeModel> ActionModel
			{
				get { return mActionModel; }
			}

			/// <summary>
			/// Делегат для установки значений объекту представления
			/// </summary>
			public Action<TTypeView> ActionView
			{
				get { return mActionView; }
			}

			/// <summary>
			/// Делегат для преобразования объекта представления в объект модели
			/// </summary>
			public Func<TTypeView, TTypeModel> OnConvertToModel
			{
				get { return mOnConvertToModel; }
				set { mOnConvertToModel = value; }
			}

			/// <summary>
			/// Делегат для преобразования объекта модели в объект представления
			/// </summary>
			public Func<TTypeModel, TTypeView> OnConvertToView
			{
				get { return mOnConvertToView; }
				set { mOnConvertToView = value; }
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public BindingDelegate()
			{
				mIsStringView = (typeof(TTypeView) == typeof(String));
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="model_instance">Экземпляр объекта модели</param>
			/// <param name="model_member_name">Имя члена объекта модели</param>
			/// <param name="view_instance">Экземпляр объекта представления</param>
			/// <param name="view_member_name">Имя члена объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			public BindingDelegate(System.Object model_instance, String model_member_name, System.Object view_instance, 
				String view_member_name)
			{
				SetModel(model_instance, model_member_name);
				SetView(view_instance, view_member_name);
			}
			#endregion

			#region ======================================= РАБОТА С ОБЪЕКТОМ МОДЕЛИ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта модели
			/// </summary>
			/// <remarks>
			/// Предполагается что остальные параметры привязки со стороны объекта модели уже корректно настроены
			/// </remarks>
			/// <param name="model_instance">Экземпляр объекта модели</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetModel(System.Object model_instance)
			{
				ResetModel(model_instance);
				if (mMode != TBindingMode.ViewData)
				{
					String member_name_model = mModelMemberName;
					if (mModelMemberType == TBindingMemberType.Property)
					{
						member_name_model = "set_" + mModelMemberName;
					}
					try
					{
						mActionModel = (Action<TTypeModel>)Delegate.CreateDelegate(typeof(Action<TTypeModel>), model_instance, member_name_model);
					}
					catch (Exception exc)
					{
#if (UNITY_2017_1_OR_NEWER)
						UnityEngine.Debug.LogException(exc);
#else
						XLogger.LogException(exc);
#endif
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта модели
			/// </summary>
			/// <param name="model_instance">Экземпляр объекта модели</param>
			/// <param name="member_name">Имя члена объекта модели</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetModel(System.Object model_instance, String member_name)
			{
				ResetModel(model_instance);

				if (SetMemberType(model_instance, member_name, ref mModelMemberType) != null)
				{
					mModelMemberName = member_name;
					if (mMode != TBindingMode.ViewData)
					{
						String member_name_model = mModelMemberName;
						if (mModelMemberType == TBindingMemberType.Property)
						{
							member_name_model = "set_" + mModelMemberName;
						}
						try
						{
							mActionModel = (Action<TTypeModel>)Delegate.CreateDelegate(typeof(Action<TTypeModel>), model_instance, member_name_model);
						}
						catch (Exception exc)
						{
#if (UNITY_2017_1_OR_NEWER)
							UnityEngine.Debug.LogException(exc);
#else
							XLogger.LogException(exc);
#endif
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения привязанного свойства/метода объекта модели
			/// </summary>
			/// <remarks>
			/// Хотя мы всегда должны знать о значении свойства, на которые подписались, однако иногда надо принудительно
			/// его запросить, например, во время присоединения
			/// </remarks>
			/// <returns>Значение привязанного свойства/метода объекта модели</returns>
			//---------------------------------------------------------------------------------------------------------
			public override System.Object GetModelValue()
			{
				// Проверяем сначала свойство 
				if (XReflection.ContainsProperty(mModelInstance, mModelMemberName))
				{
					return (XReflection.GetPropertyValue(mModelInstance, mModelMemberName));
				}
				else
				{
					// Теперь поле
					return (XReflection.GetFieldValue(mModelInstance, mModelMemberName));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление данных объекта модели
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			protected override void UpdateModelProperty(Object sender, PropertyChangedEventArgs args)
			{
				if (mIsEnabled)
				{
					if (mModelMemberName == args.PropertyName)
					{
						// Используется интерфейс INotifyPropertyChanged
						if (mModelPropertyChanged != null)
						{
							// Получаем актуальное значение
							System.Object value = GetModelValue();
							if (mOnConvertToView != null)
							{
								mActionView(mOnConvertToView((TTypeModel)value));
							}
							else
							{
								if (mIsStringView)
								{
									mActionView((TTypeView)(System.Object)value.ToString());
								}
								else
								{
									mActionView((TTypeView)value);
								}
							}
						}
					}
				}
			}
			#endregion

			#region ======================================= РАБОТА С ОБЪЕКТОМ ПРЕДСТАВЛЕНИЯ ===========================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта представления
			/// </summary>
			/// <remarks>
			/// Предполагается что остальные параметры привязки со стороны объекта представления уже корректно настроены
			/// </remarks>
			/// <param name="view_instance">Экземпляр объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetView(System.Object view_instance)
			{
				ResetView(view_instance);
				if (mMode != TBindingMode.DataManager)
				{
					String member_name_view = mViewMemberName;
					if (mViewMemberType == TBindingMemberType.Property)
					{
						member_name_view = "set_" + mViewMemberName;
					}
					try
					{
						mActionView = (Action<TTypeView>)Delegate.CreateDelegate(typeof(Action<TTypeView>), view_instance, member_name_view);
					}
					catch (Exception exc)
					{
#if (UNITY_2017_1_OR_NEWER)
						UnityEngine.Debug.LogException(exc);
#else
						XLogger.LogException(exc);
#endif
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установка объекта представления
			/// </summary>
			/// <param name="view_instance">Экземпляр объекта представления</param>
			/// <param name="member_name">Имя члена объекта представления</param>
			//---------------------------------------------------------------------------------------------------------
			public override void SetView(System.Object view_instance, String member_name)
			{
				mIsStringView = (typeof(TTypeView) == typeof(String));
				ResetView(view_instance);
				if (SetMemberType(view_instance, member_name, ref mViewMemberType) != null)
				{
					mViewMemberName = member_name;
					if (mMode != TBindingMode.DataManager)
					{
						String member_name_view = mViewMemberName;
						if (mViewMemberType == TBindingMemberType.Property)
						{
							member_name_view = "set_" + mViewMemberName;
						}
						try
						{
							mActionView = (Action<TTypeView>)Delegate.CreateDelegate(typeof(Action<TTypeView>), view_instance, member_name_view);
						}
						catch (Exception exc)
						{
#if (UNITY_2017_1_OR_NEWER)
							UnityEngine.Debug.LogException(exc);
#else
							XLogger.LogException(exc);
#endif
						}
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение значения привязанного свойства/метода объекта представления
			/// </summary>
			/// <remarks>
			/// Хотя мы всегда должны знать о значении свойства, на которые подписались, однако иногда надо принудительно
			/// его запросить, например, во время присоединения
			/// </remarks>
			/// <returns>Значение привязанного свойства/метода объекта представления</returns>
			//---------------------------------------------------------------------------------------------------------
			public override System.Object GetViewValue()
			{
				// Проверяем сначала свойство 
				if (XReflection.ContainsProperty(mViewInstance, mViewMemberName))
				{
					return (XReflection.GetPropertyValue(mViewInstance, mViewMemberName));
				}
				else
				{
					// Теперь поле
					return (XReflection.GetFieldValue(mViewInstance, mViewMemberName));
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление данных объекта представления
			/// </summary>
			/// <param name="sender">Источник события</param>
			/// <param name="args">Аргументы события</param>
			//---------------------------------------------------------------------------------------------------------
			protected override void UpdateViewProperty(Object sender, PropertyChangedEventArgs args)
			{
				if (mIsEnabled)
				{
					if (mViewMemberName == args.PropertyName)
					{
						// Используется интерфейс INotifyPropertyChanged
						if (mViewPropertyChanged != null)
						{
							// Получаем актуальное значение
							System.Object value = GetModelValue();

							if (mOnConvertToModel != null)
							{
								mActionModel(mOnConvertToModel((TTypeView)value));
							}
							else
							{
								mActionModel((TTypeModel)value);
							}
						}
					}
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================