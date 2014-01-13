
package at.wea5.geocaching.webserviceproxy;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java-Klasse für anonymous complex type.
 * 
 * <p>Das folgende Schemafragment gibt den erwarteten Content an, der in dieser Klasse enthalten ist.
 * 
 * <pre>
 * &lt;complexType>
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="FindCachesByCacheDifficultyResult" type="{http://GeoCaching.Services/}ArrayOfCache" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "findCachesByCacheDifficultyResult"
})
@XmlRootElement(name = "FindCachesByCacheDifficultyResponse")
public class FindCachesByCacheDifficultyResponse {

    @XmlElement(name = "FindCachesByCacheDifficultyResult")
    protected ArrayOfCache findCachesByCacheDifficultyResult;

    /**
     * Ruft den Wert der findCachesByCacheDifficultyResult-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfCache }
     *     
     */
    public ArrayOfCache getFindCachesByCacheDifficultyResult() {
        return findCachesByCacheDifficultyResult;
    }

    /**
     * Legt den Wert der findCachesByCacheDifficultyResult-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfCache }
     *     
     */
    public void setFindCachesByCacheDifficultyResult(ArrayOfCache value) {
        this.findCachesByCacheDifficultyResult = value;
    }

}
