using UnityEngine;

namespace UIModule.Extensions
{
	public static class UIExtensions
	{
		/// <summary>
		/// Сhecks if the borders of the child rect element are included in the parent horizontal rectangle
		/// </summary>
		/// <description>
		/// Can check containing of the top left and top right corner of the rect rectangle in the parent rectangle
		/// </description>
		/// <param name="__parentRect">
		///	parent RectTransform
		/// </param>
		/// <param name="__childRect">
		/// child RectTransform
		/// </param>
		/// <param name="__checkLeftUPCorner">
		/// need check left bounds?
		/// </param>
		/// <param name="__checkRightUPCorner">
		///	need check right bounds?
		/// </param>
		/// <returns>
		/// Returns true - if contains, otherwise - false	
		/// </returns>
		public static bool IsInsideHorizontal(this RectTransform __parentRect, RectTransform __childRect, 
			bool __checkLeftUPCorner, bool __checkRightUPCorner)
		{
			Vector3[] childCorners = new Vector3[4];
			__childRect.GetWorldCorners(childCorners);

			Vector3[] parentCorners = new Vector3[4];
			__parentRect.GetWorldCorners(parentCorners);

			if (__checkLeftUPCorner)
				if (childCorners[1].x <= parentCorners[1].x)
					return false;
			
			if (__checkRightUPCorner)
				if (childCorners[2].x >= parentCorners[2].x)
					return false;

			return true;
		}
	}
}
