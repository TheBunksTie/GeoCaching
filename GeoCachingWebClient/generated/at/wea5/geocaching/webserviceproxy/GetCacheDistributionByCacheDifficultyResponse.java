
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
 *         &lt;element name="GetCacheDistributionByCacheDifficultyResult" type="{http://GeoCaching.Services/}StatisticDataset" minOccurs="0"/>
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
    "getCacheDistributionByCacheDifficultyResult"
})
@XmlRootElement(name = "GetCacheDistributionByCacheDifficultyResponse")
public class GetCacheDistributionByCacheDifficultyResponse {

    @XmlElement(name = "GetCacheDistributionByCacheDifficultyResult")
    protected StatisticDataset getCacheDistributionByCacheDifficultyResult;

    /**
     * Ruft den Wert der getCacheDistributionByCacheDifficultyResult-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link StatisticDataset }
     *     
     */
    public StatisticDataset getGetCacheDistributionByCacheDifficultyResult() {
        return getCacheDistributionByCacheDifficultyResult;
    }

    /**
     * Legt den Wert der getCacheDistributionByCacheDifficultyResult-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link StatisticDataset }
     *     
     */
    public void setGetCacheDistributionByCacheDifficultyResult(StatisticDataset value) {
        this.getCacheDistributionByCacheDifficultyResult = value;
    }

}
