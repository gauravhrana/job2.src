﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Framework.Components.DataAccess.ThirdParty
{
	//http://ntsblog.homedev.com.au/index.php/2010/05/06/c-stack-with-maximum-limit/
    /// <summary>
    /// Generic stack implementation with a maximum limit
    /// When something is pushed on the last item is removed from the list
    /// </summary>
    [Serializable]
    public class MaxStack<T>
    {
        #region Fields
 
        private int _limit;
        private LinkedList<T> _list;
         
        #endregion
 
        #region Constructors
 
        public MaxStack(int maxSize)
        {
            _limit = maxSize;
            _list = new LinkedList<T>();             
        }
 
        #endregion
 
        #region Public Stack Implementation
 
        public void Push(T value)
        {
            if (_list.Count == _limit)
            {
                _list.RemoveLast();
            }
            _list.AddFirst(value);
        }
 
        public T Pop()
        {
            if (_list.Count > 0)
            {
                T value = _list.First.Value;
                _list.RemoveFirst();
                return value;
            }
            else
            {
                throw new InvalidOperationException("The Stack is empty");
            }              
        }
 
        public T Peek()
        {
            if (_list.Count > 0)
            {
                T value = _list.First.Value;
                return value;
            }
            else
            {
                throw new InvalidOperationException("The Stack is empty");
            }             
        }
 
        public void Clear()
        {
            _list.Clear();             
        }
 
        public int Count
        {
            get { return _list.Count; }
        }
 
        /// <summary>
        /// Checks if the top object on the stack matches the value passed in
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsTop(T value)
        {
            var result = false;
            if (Count > 0)
            
			{
                result = Peek().Equals(value);
            }
            
			return result;
        }
 
        public bool Contains(T value)
        {
            var result = false;
            
			if (Count > 0)
            {
                result = _list.Contains(value);
            }

            return result;
        }
 
		public IEnumerator GetEnumerator()
		{
			return _list.GetEnumerator();
		}
 
        #endregion
    }

	public class FixedSizeStack : Stack
	{
		private readonly int _maxNumber;

		public FixedSizeStack(int limit) : base()
		{
			_maxNumber = limit;
		}

		public override void Push(object obj)
		{
			if (Count < _maxNumber)
				base.Push(obj);
		}

		public FixedSizeStack Clone(int maxLength)
		{			
			var oStack = new FixedSizeStack(maxLength);
					
			for (var i = Count - 1; i > -1; i--)
			{
				oStack.Push(Pop());					
			}							

			// put it order it was in original.
			var result = new FixedSizeStack(maxLength);

			for (var i = oStack.Count - 1; i > -1; i--)
			{
				result.Push(oStack.Pop());					
			}							

			return result;
		}    
	}
}
