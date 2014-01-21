
package at.wea5.geocaching.webserviceproxy;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
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
 *         &lt;element name="requestingUser" type="{http://GeoCaching.Services/}User" minOccurs="0"/>
 *         &lt;element name="logEntry" type="{http://GeoCaching.Services/}LogEntry" minOccurs="0"/>
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
    "requestingUser",
    "logEntry"
})
@XmlRootElement(name = "AddLogEntryForCache")
public class AddLogEntryForCache {

    protected User requestingUser;
    protected LogEntry logEntry;

    /**
     * Ruft den Wert der requestingUser-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link User }
     *     
     */
    public User getRequestingUser() {
        return requestingUser;
    }

    /**
     * Legt den Wert der requestingUser-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link User }
     *     
     */
    public void setRequestingUser(User value) {
        this.requestingUser = value;
    }

    /**
     * Ruft den Wert der logEntry-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link LogEntry }
     *     
     */
    public LogEntry getLogEntry() {
        return logEntry;
    }

    /**
     * Legt den Wert der logEntry-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link LogEntry }
     *     
     */
    public void setLogEntry(LogEntry value) {
        this.logEntry = value;
    }

}
