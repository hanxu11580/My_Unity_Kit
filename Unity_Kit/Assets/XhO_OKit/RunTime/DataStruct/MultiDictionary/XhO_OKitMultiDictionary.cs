using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


namespace XhO_OKit.DataStruct
{

    public class XhO_OKitMultiDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, XhO_OKitLinkedListRange<TValue>>>
    {
        /// <summary>
        /// 利用编写好的可回收node链表，辅助管理
        /// </summary>
        private readonly XhO_OKitLinkedList<TValue> _LinkedList;

        private readonly Dictionary<TKey, XhO_OKitLinkedListRange<TValue>> _rangeDict;

        public XhO_OKitMultiDictionary()
        {
            _LinkedList = new XhO_OKitLinkedList<TValue>();
            _rangeDict = new Dictionary<TKey, XhO_OKitLinkedListRange<TValue>>();
        }

        /// <summary>
        /// 字典内个数
        /// </summary>
        public int Count => _rangeDict.Count;

        /// <summary>
        /// 返回字典中某范围（链表）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public XhO_OKitLinkedListRange<TValue> this[TKey key]
        {
            get
            {
                _rangeDict.TryGetValue(key, out XhO_OKitLinkedListRange<TValue> range);
                return range;
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _LinkedList.Clear();
            _rangeDict.Clear();
        }

        /// <summary>
        /// 是否存在此键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(TKey key)
        {
            return _rangeDict.ContainsKey(key);
        }

        /// <summary>
        /// 检查范围中是否包含某值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(TKey key, TValue value)
        {
            if(_rangeDict.TryGetValue(key, out XhO_OKitLinkedListRange<TValue> range))
            {
                return range.Contains(value);
            }
            return false;
        }

        /// <summary>
        /// 尝试获得此键值范围
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out XhO_OKitLinkedListRange<TValue> range)
        {
            return _rangeDict.TryGetValue(key, out range);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            if(_rangeDict.TryGetValue(key, out XhO_OKitLinkedListRange<TValue> range))
            {
                _LinkedList.AddBefore(range.Terminal, value);
                //后修改：原来导致重复添加
                return;
            }

            LinkedListNode<TValue> first = _LinkedList.AddLast(value);
            LinkedListNode<TValue> terminal = _LinkedList.AddLast(default);
            _rangeDict.Add(key, new XhO_OKitLinkedListRange<TValue>(first, terminal));
        }

        /// <summary>
        /// 从指定的主键中移除指定的值。
        /// </summary>
        /// <param name="key">指定的主键。</param>
        /// <param name="value">指定的值。</param>
        /// <returns>是否移除成功。</returns>
        public bool Remove(TKey key, TValue value)
        {
            if (_rangeDict.TryGetValue(key, out XhO_OKitLinkedListRange<TValue> range))
            {
                for (LinkedListNode<TValue> current = range.First;
                    current != null && current != range.Terminal;
                    current = current.Next)
                {
                    if (current.Value.Equals(value))
                    {
                        if (current == range.First)
                        {
                            //如果是第一个
                            LinkedListNode<TValue> next = current.Next;
                            if (next == range.Terminal)
                            {
                                _LinkedList.Remove(next);
                                _rangeDict.Remove(key);
                            }
                            else
                            {
                                //next=first
                                _rangeDict[key] = new XhO_OKitLinkedListRange<TValue>(next, range.Terminal);
                            }
                        }
                        //如果是中间
                        _LinkedList.Remove(current);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 从指定的主键中移除所有的值。
        /// </summary>
        /// <param name="key">指定的主键。</param>
        /// <returns>是否移除成功。</returns>
        public bool RemoveAll(TKey key)
        {
            if (_rangeDict.TryGetValue(key, out XhO_OKitLinkedListRange<TValue> range))
            {
                _rangeDict.Remove(key);

                //这里考虑node回收所以利用_LinkedList删除
                //这里可以封装到sfmlinkedlist方法中
                LinkedListNode<TValue> current = range.First;
                while (current != null)
                {
                    LinkedListNode<TValue> next = current != range.Terminal ? current.Next : null;
                    _LinkedList.Remove(current);
                    current = next;
                }
                return true;
            }
            return false;
        }

        public IEnumerator<KeyValuePair<TKey, XhO_OKitLinkedListRange<TValue>>> GetEnumerator()
        {
            return new Enumerator(_rangeDict);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(_rangeDict);
        }

        #region 可foreach

        /// <summary>
        /// 循环访问集合的枚举数。
        /// </summary>
        [StructLayout(LayoutKind.Auto)]
        public struct Enumerator : IEnumerator<KeyValuePair<TKey, XhO_OKitLinkedListRange<TValue>>>, IEnumerator
        {
            private Dictionary<TKey, XhO_OKitLinkedListRange<TValue>>.Enumerator m_Enumerator;

            internal Enumerator(Dictionary<TKey, XhO_OKitLinkedListRange<TValue>> dictionary)
            {
                if (dictionary == null)
                {
                    Debug.LogError("Dictionary is invalid.");
                }

                m_Enumerator = dictionary.GetEnumerator();
            }

            /// <summary>
            /// 获取当前结点。
            /// </summary>
            public KeyValuePair<TKey, XhO_OKitLinkedListRange<TValue>> Current
            {
                get
                {
                    return m_Enumerator.Current;
                }
            }

            /// <summary>
            /// 获取当前的枚举数。
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    return m_Enumerator.Current;
                }
            }

            /// <summary>
            /// 清理枚举数。
            /// </summary>
            public void Dispose()
            {
                m_Enumerator.Dispose();
            }

            /// <summary>
            /// 获取下一个结点。
            /// </summary>
            /// <returns>返回下一个结点。</returns>
            public bool MoveNext()
            {
                return m_Enumerator.MoveNext();
            }

            /// <summary>
            /// 重置枚举数。
            /// </summary>
            void IEnumerator.Reset()
            {
                ((IEnumerator<KeyValuePair<TKey, XhO_OKitLinkedListRange<TValue>>>)m_Enumerator).Reset();
            }
        }
        #endregion
    }
}
