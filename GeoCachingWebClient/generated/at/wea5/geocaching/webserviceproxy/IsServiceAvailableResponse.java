
package at.wea5.geocaching.webserviceproxy;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java-Klasse f�r anonymous complex type.
 * 
 * <p>Das folgende Schemafragment gibt den erwarteten Content an, der in dieser Klasse enthalten ist.
 * 
 * <pre>
 * &lt;complexType>
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="IsServiceAvailableResult" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
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
    "isServiceAvailableResult"
})
@XmlRootElement(name = "IsServiceAvailableResponse")
public class IsServiceAvailableResponse {

    @XmlElement(name = "IsServiceAvailableResult")
    protected boolean isServiceAvailableResult;

    /**
     * Ruft den Wert der isServiceAvailableResult-Eigenschaft ab.
     * 
     */
    public boolean isIsServiceAvailableResult() {
        return isServiceAvailableResult;
    }

    /**
     * Legt den Wert der isServiceAvailableResult-Eigenschaft fest.
     * 
     */
    public void setIsServiceAvailableResult(boolean value) {
        this.isServiceAvailableResult = value;
    }

}
