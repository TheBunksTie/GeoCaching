
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
 *         &lt;element name="ComputeDefaultFilterResult" type="{http://GeoCaching.Services/}CacheFilter" minOccurs="0"/>
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
    "computeDefaultFilterResult"
})
@XmlRootElement(name = "ComputeDefaultFilterResponse")
public class ComputeDefaultFilterResponse {

    @XmlElement(name = "ComputeDefaultFilterResult")
    protected CacheFilter computeDefaultFilterResult;

    /**
     * Ruft den Wert der computeDefaultFilterResult-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link CacheFilter }
     *     
     */
    public CacheFilter getComputeDefaultFilterResult() {
        return computeDefaultFilterResult;
    }

    /**
     * Legt den Wert der computeDefaultFilterResult-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link CacheFilter }
     *     
     */
    public void setComputeDefaultFilterResult(CacheFilter value) {
        this.computeDefaultFilterResult = value;
    }

}
