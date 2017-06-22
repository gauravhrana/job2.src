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
    public class DictionaryList2<TKey, TClass> : 
        IList<TKey>, IDictionary<TKey, TClass>        
    {
        #region attributes 
        private List<TClass> m_List;
        private List<TKey> m_KeyList;         
        private Dictionary<TKey, int> m_Dictionary; 
        #endregion

        /// <summary>
        /// Dictionary List
        /// </summary>
        public DictionaryList2()
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

        #region IList<TKey> Members

        public int IndexOf(TKey itemKey)
        {
            int indexLocation;
            return 
                ((m_Dictionary.TryGetValue(itemKey, out indexLocation)) ? indexLocation : -1); 
            
        }

        public void Insert(int index, TKey item)
        {
            if (item == null)
                throw new ArgumentNullException("Value cannot be null");

            if (m_Dictionary.ContainsKey(item))
                throw new ArgumentException("Key already exists");

            m_KeyList.Insert(index, item);
            m_List.Insert(index, default(TClass));
            m_Dictionary.Add(item, index);

            UpdateDictionaryIndexes(index);
        }

        public void RemoveAt(int index)
        {
            m_Dictionary.Remove(m_KeyList[index]); 
            m_List.RemoveAt(index);
            m_KeyList.RemoveAt(index);

            UpdateDictionaryIndexes(index); 
        }

        private void UpdateDictionaryIndexes(int startIndex)
        {
            //Set new indexes. 
            for (int i = startIndex; i < m_List.Count; i++)
            {
                m_Dictionary[m_KeyList[i]] = i;
            }
        }

        public virtual TKey this[int index]
        {
            get
            {
                return m_KeyList[index];
            }
            set
            {
                Insert(index, value); 
                
            }
        }

        #endregion

        #region ICollection<TKey> Members

        public void Add(TKey item)
        {
            if (item == null)
                throw new ArgumentNullException("Value cannot be null");

            if (m_Dictionary.ContainsKey(item))
                throw new ArgumentException("Key already exists");

            m_KeyList.Add(item);
            m_List.Add(default(TClass));            
            m_Dictionary.Add(item, m_KeyList.Count - 1); 
        }

        public void Clear()
        {
            m_List.Clear();
            m_KeyList.Clear(); 
            m_Dictionary.Clear(); 
        }

        public bool Contains(TKey item)
        {
            return m_Dictionary.ContainsKey(item);
        }

        public bool ContainsKey(TKey key)
        {
            return m_Dictionary.ContainsKey(key); 
        }

        public void CopyTo(TKey[] array, int arrayIndex)
        {
            m_KeyList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return m_KeyList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(TKey key)
        {
            int indexLocation; 
            if(m_Dictionary.TryGetValue(key, out indexLocation))
            {
                RemoveAt(indexLocation);
                return true; 
            }

            return false; 
        }

        #endregion

        #region IEnumerable<TKey> Members

        public IEnumerator<TKey> GetEnumerator()
        {
            return (IEnumerator<TKey>)m_KeyList.GetEnumerator();
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
