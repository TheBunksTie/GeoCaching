
package at.wea5.geocaching.webserviceproxy;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
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
 *         &lt;element name="requestingUser" type="{http://GeoCaching.Services/}User" minOccurs="0"/>
 *         &lt;element name="rating" type="{http://GeoCaching.Services/}Rating" minOccurs="0"/>
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
    "rating"
})
@XmlRootElement(name = "AddRatingForCache")
public class AddRatingForCache {

    protected User requestingUser;
    protected Rating rating;

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
     * Ruft den Wert der rating-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link Rating }
     *     
     */
    public Rating getRating() {
        return rating;
    }

    /**
     * Legt den Wert der rating-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link Rating }
     *     
     */
    public void setRating(Rating value) {
        this.rating = value;
    }

}
