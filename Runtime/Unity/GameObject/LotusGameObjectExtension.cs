//=====================================================================================================================
// Проект: LotusPlatform
// Раздел: Модуль базового ядра
// Подраздел: Подсистема игровых объектов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusGameObjectExtension.cs
*		Методы расширения функциональности игрового объекта.
*		Реализация методов расширения функциональности игрового объекта Unity и направлены на достижение более эффективной
*	работы и упрощения типовых действий с игровыми объектами.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreUnityGameObject
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы расширений игрового объекта Unity
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XExtensionGameObject
		{
			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на статус префаба игрового объекта
			/// </summary>
			/// <param name="this">Игровой объект</param>
			/// <returns>Статус префаба игрового объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsPrefab(this GameObject @this)
			{
				return (@this.scene.name == null);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение полного пути игрового объект в иерархии игровых объектов
			/// </summary>
			/// <param name="this">Игровой объект</param>
			/// <returns>Полный путь</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetPathScene(this GameObject @this)
			{
				StringBuilder path = new StringBuilder(40);
				while (@this.transform.parent != null)
				{
					@this = @this.transform.parent.gameObject;
					if (@this.transform.parent != null)
					{
						path.Insert(0, @this.name);
						path.Insert(0, "/");
					}
					else
					{
						path.Insert(0, @this.name);
					}
				}

				return (path.ToString());
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Гарантирование обеспечение компонента (который может быть только один на игровом объекте)
			/// </summary>
			/// <param name="this">Игровой объект</param>
			/// <param name="type_component">Тип компонента</param>
			/// <returns>Добавленный или существующий компонент</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Component EnsureComponent(this GameObject @this, Type type_component)
			{
#if UNITY_EDITOR
				if (@this == null)
				{
					return null;
				}
#endif
				Component component = @this.GetComponent(type_component);
				if (component == null)
				{
					component = @this.AddComponent(type_component);
				}

				return component;
			}
			#endregion

			#region ======================================= МЕТОДЫ ПОИСКА =============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Поиск игрового среди дочерних объектов по указанному имени
			/// </summary>
			/// <param name="this">Игровой объект</param>
			/// <param name="child_name">Имя дочернего игрового объекта</param>
			/// <param name="in_children">Дополнительный поиск среди дочерних объектов второго уровня</param>
			/// <returns>Найденный игровой объект или null</returns>
			//---------------------------------------------------------------------------------------------------------
			public static GameObject FindInChildren(this GameObject @this, String child_name, Boolean in_children = false)
			{
				// Поиск среди дочерних объектов
				Transform this_transform = @this.transform;
				for (Int32 i = 0; i < this_transform.childCount; i++)
				{
					Transform tran = this_transform.GetChild(i);
					if (tran != null && tran.name == child_name)
					{
						return (tran.gameObject);
					}
				}

				if (in_children)
				{
					for (Int32 ic = 0; ic < this_transform.childCount; ic++)
					{
						Transform transform_child = this_transform.GetChild(ic).transform;
						if (transform_child != null && transform_child.childCount > 0)
						{
							for (Int32 c = 0; c < transform_child.childCount; c++)
							{
								Transform tran = transform_child.GetChild(c);
								if (tran != null && tran.name == child_name)
								{
									return (tran.gameObject);
								}
							}
						}
					}
				}

				return null;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================