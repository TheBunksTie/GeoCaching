
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
 *         &lt;element name="FindCachesByTerrainDifficultyResult" type="{http://GeoCaching.Services/}ArrayOfCache" minOccurs="0"/>
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
    "findCachesByTerrainDifficultyResult"
})
@XmlRootElement(name = "FindCachesByTerrainDifficultyResponse")
public class FindCachesByTerrainDifficultyResponse {

    @XmlElement(name = "FindCachesByTerrainDifficultyResult")
    protected ArrayOfCache findCachesByTerrainDifficultyResult;

    /**
     * Ruft den Wert der findCachesByTerrainDifficultyResult-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfCache }
     *     
     */
    public ArrayOfCache getFindCachesByTerrainDifficultyResult() {
        return findCachesByTerrainDifficultyResult;
    }

    /**
     * Legt den Wert der findCachesByTerrainDifficultyResult-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfCache }
     *     
     */
    public void setFindCachesByTerrainDifficultyResult(ArrayOfCache value) {
        this.findCachesByTerrainDifficultyResult = value;
    }

}
