using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace SYFramework.Net.Common
{
    /// <summary>
    /// XML帮助类
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// 读取XML指定节点
        /// </summary>
        /// <param name="xmlFilePath">XML文件的路径</param>
        /// <param name="xpath">XPath路径</param>
        /// <returns></returns>
        public static string ReadXmlNode(string xmlFilePath, string xpath)
        {
            string result = string.Empty;
            try
            {
                XPathDocument doc = new XPathDocument(xmlFilePath);
                XPathNavigator nav = doc.CreateNavigator();
                XPathNodeIterator ite = nav.Select(xpath);
                ite.MoveNext();
                result = ite.Current.Value;
                ite = null;
                nav = null;
                doc = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 读取XML指定节点集合
        /// </summary>
        /// <param name="xmlFilePath">XML文件的路径</param>
        /// <param name="xpath">XPath路径</param>
        /// <returns></returns>
        public static List<string> ReadXmlNodeList(string xmlFilePath, string xpath)
        {
            List<string> lists = null;
            try
            {
                XPathDocument doc = new XPathDocument(xmlFilePath);
                XPathNavigator nav = doc.CreateNavigator();
                XPathNodeIterator ite = nav.Select(xpath);
                foreach (XPathNavigator node in ite)
                {
                    if (lists == null)
                        lists = new List<string>();
                    lists.Add(node.InnerXml);
                }
                ite = null;
                nav = null;
                doc = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lists;
        }
        /// <summary>
        /// 写入XML指定节点的值
        /// </summary>
        /// <param name="xmlFilePath">XML文件路径</param>
        /// <param name="xpath">XPath路径</param>
        /// <param name="newValue">改变的值</param>
        public static void WriteXmlNode(string xmlFilePath, string xpath, string newValue)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFilePath);
                XmlNode node = doc.DocumentElement.SelectSingleNode(xpath);
                node.InnerText = newValue;
                doc.Save(xmlFilePath);
                node = null;
                doc = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
