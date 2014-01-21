
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
 *         &lt;element name="GetCachesHiddenByUserResult" type="{http://GeoCaching.Services/}StatisticDataset" minOccurs="0"/>
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
    "getCachesHiddenByUserResult"
})
@XmlRootElement(name = "GetCachesHiddenByUserResponse")
public class GetCachesHiddenByUserResponse {

    @XmlElement(name = "GetCachesHiddenByUserResult")
    protected StatisticDataset getCachesHiddenByUserResult;

    /**
     * Ruft den Wert der getCachesHiddenByUserResult-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link StatisticDataset }
     *     
     */
    public StatisticDataset getGetCachesHiddenByUserResult() {
        return getCachesHiddenByUserResult;
    }

    /**
     * Legt den Wert der getCachesHiddenByUserResult-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link StatisticDataset }
     *     
     */
    public void setGetCachesHiddenByUserResult(StatisticDataset value) {
        this.getCachesHiddenByUserResult = value;
    }

}
