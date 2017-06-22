using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Framework.Components.DataAccess
{
	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class DevelopementHelper
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static string GetMyMethodName(int level = 1)
		{
			//if (level >= st.FrameCount)
			//{
			//    level = st.FrameCount - 1;
			//}

			try
			{
				var st = new StackTrace(new StackFrame(level));

				return st.GetFrame(0).GetMethod().Name;
			}
			catch (Exception)
			{
				//
			}

			return String.Empty;
		}
	}
}