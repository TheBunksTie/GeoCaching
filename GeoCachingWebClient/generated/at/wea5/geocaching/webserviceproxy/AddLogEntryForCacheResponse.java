
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
 *         &lt;element name="AddLogEntryForCacheResult" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
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
    "addLogEntryForCacheResult"
})
@XmlRootElement(name = "AddLogEntryForCacheResponse")
public class AddLogEntryForCacheResponse {

    @XmlElement(name = "AddLogEntryForCacheResult")
    protected boolean addLogEntryForCacheResult;

    /**
     * Ruft den Wert der addLogEntryForCacheResult-Eigenschaft ab.
     * 
     */
    public boolean isAddLogEntryForCacheResult() {
        return addLogEntryForCacheResult;
    }

    /**
     * Legt den Wert der addLogEntryForCacheResult-Eigenschaft fest.
     * 
     */
    public void setAddLogEntryForCacheResult(boolean value) {
        this.addLogEntryForCacheResult = value;
    }

}
