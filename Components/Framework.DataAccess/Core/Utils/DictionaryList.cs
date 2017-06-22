using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Framework.CommonServices.BusinessDomain.Utils
{
    /// <summary>
    /// Class which behaves like both a dictionary and a list
    /// </summary>
    /// <typeparam name="TKey">key type</typeparam>
    /// <typeparam name="TClass">value type</typeparam>
    [Serializable]
    public class DictionaryList<TKey, TClass> : 
        IList<TClass>, IDictionary<TKey, TClass>        
    {
        #region attributes 
        private List<TClass> m_List;
        private List<TKey> m_KeyList;         
        private Dictionary<TKey, int> m_Dictionary; 
        #endregion

        /// <summary>
        /// Dictionary List
        /// </summary>
        public DictionaryList()
        {
            
            m_Dictionary = new Dictionary<TKey, int>();
            m_List = new List<TClass>();
            m_KeyList = new List<TKey>();
        }
        
        public bool TryGetValue(TKey key, out TClass value)
        {
            int indexLocation;
            value = default(TClass); 
            if (m_Dictionary.TryGetValue(key, out indexLocation))
            {                
                value = m_List[indexLocation]; 
                return true; 
            }

            return false; 
        }

        public ReadOnlyCollection<TClass> AsReadOnly()
        {
            return (ReadOnlyCollection<TClass>)m_List.AsReadOnly(); 
        }

        #region IList<TClass> Members

        public int IndexOf(TClass item)
        {
            return m_List.IndexOf(item);
        }

        public void Insert(int index, TClass item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            m_Dictionary.Remove(m_KeyList[index]); 
            m_List.RemoveAt(index);
            m_KeyList.RemoveAt(index);
            
            //Set new indexes. 
            for (int i = index; i < m_List.Count; i++)
            {
                m_Dictionary[m_KeyList[i]] = i; 
            }
        }

        public virtual TClass this[int index]
        {
            get
            {
                return m_List[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection<TClass> Members

        public void Add(TClass item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            m_List.Clear();
            m_KeyList.Clear(); 
            m_Dictionary.Clear(); 
        }

        public bool Contains(TClass item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            return m_Dictionary.ContainsKey(key); 
        }

        public void CopyTo(TClass[] array, int arrayIndex)
        {
            m_List.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return m_List.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TClass item)
        {
            int indexLocation = m_List.IndexOf(item);
            if (indexLocation != -1)
            {
                RemoveAt(indexLocation);
                return true; 
            }

            return false; 
        }

        #endregion

        #region IEnumerable<TClass> Members

        public IEnumerator<TClass> GetEnumerator()
        {
            return (IEnumerator<TClass>)m_List.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_List.GetEnumerator();
        }

        #endregion

        #region IDictionary<TKey,TClass> Members

        public virtual void Add(TKey key, TClass value)
        {
            m_List.Add(value);
            m_KeyList.Add(key); 
            m_Dictionary.Add(key, m_List.Count - 1);
            
        }

        public ICollection<TKey> Keys
        {
            get { return (ICollection<TKey>)m_KeyList; }
        }

        /// <summary>
        /// not implemented
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            int indexLocation;
            if (m_Dictionary.TryGetValue(key, out indexLocation))
            {
                RemoveAt(indexLocation);
                return true; 
            }
            return false; 
        }

        public ICollection<TClass> Values
        {
            get { return (ICollection<TClass>)m_List; }
        }

        public virtual TClass this[TKey key]
        {
            get
            {
                int indexLocation;
                if (m_Dictionary.TryGetValue(key, out indexLocation))
                {
                    return m_List[indexLocation];
                }
                return default(TClass); 
            }
            set
            {                
                m_List.Add(value);
                m_KeyList.Add(key);
                m_Dictionary[key] = m_List.Count - 1;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TClass>> Members

        public virtual void Add(KeyValuePair<TKey, TClass> item)
        {            
            m_List.Add(item.Value);
            m_KeyList.Add(item.Key);
            m_Dictionary.Add(item.Key, m_List.Count - 1);
        }

        public bool Contains(KeyValuePair<TKey, TClass> item)
        {
            return m_Dictionary.ContainsKey(item.Key);
        }

        public void CopyTo(KeyValuePair<TKey, TClass>[] array, int arrayIndex)
        {
            for(int index = 0; index < m_List.Count; index++) 
            {
                array[index + arrayIndex] = new KeyValuePair<TKey, TClass>(m_KeyList[index], m_List[index]);
            }
        }

        public bool Remove(KeyValuePair<TKey, TClass> item)
        {
            int indexLocation;
            if (m_Dictionary.TryGetValue(item.Key, out indexLocation))
            {
                RemoveAt(indexLocation);
                return true;
            }
            return false; 
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TClass>> Members

        IEnumerator<KeyValuePair<TKey, TClass>> IEnumerable<KeyValuePair<TKey, TClass>>.GetEnumerator()
        {
            for (int i = 0; i < m_List.Count; i++)
                yield return new KeyValuePair<TKey, TClass>(m_KeyList[i], m_List[i]); 
        }

        #endregion
    }
}
