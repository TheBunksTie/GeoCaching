
package at.wea5.geocaching.webserviceproxy;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java-Klasse für CacheDetails complex type.
 * 
 * <p>Das folgende Schemafragment gibt den erwarteten Content an, der in dieser Klasse enthalten ist.
 * 
 * <pre>
 * &lt;complexType name="CacheDetails">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="Cache" type="{http://GeoCaching.Services/}Cache" minOccurs="0"/>
 *         &lt;element name="Images" type="{http://GeoCaching.Services/}ArrayOfImage" minOccurs="0"/>
 *         &lt;element name="Rating" type="{http://www.w3.org/2001/XMLSchema}double"/>
 *         &lt;element name="LogEntries" type="{http://GeoCaching.Services/}ArrayOfLogEntry" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "CacheDetails", propOrder = {
    "cache",
    "images",
    "rating",
    "logEntries"
})
public class CacheDetails {

    @XmlElement(name = "Cache")
    protected Cache cache;
    @XmlElement(name = "Images")
    protected ArrayOfImage images;
    @XmlElement(name = "Rating")
    protected double rating;
    @XmlElement(name = "LogEntries")
    protected ArrayOfLogEntry logEntries;

    /**
     * Ruft den Wert der cache-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link Cache }
     *     
     */
    public Cache getCache() {
        return cache;
    }

    /**
     * Legt den Wert der cache-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link Cache }
     *     
     */
    public void setCache(Cache value) {
        this.cache = value;
    }

    /**
     * Ruft den Wert der images-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfImage }
     *     
     */
    public ArrayOfImage getImages() {
        return images;
    }

    /**
     * Legt den Wert der images-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfImage }
     *     
     */
    public void setImages(ArrayOfImage value) {
        this.images = value;
    }

    /**
     * Ruft den Wert der rating-Eigenschaft ab.
     * 
     */
    public double getRating() {
        return rating;
    }

    /**
     * Legt den Wert der rating-Eigenschaft fest.
     * 
     */
    public void setRating(double value) {
        this.rating = value;
    }

    /**
     * Ruft den Wert der logEntries-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfLogEntry }
     *     
     */
    public ArrayOfLogEntry getLogEntries() {
        return logEntries;
    }

    /**
     * Legt den Wert der logEntries-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfLogEntry }
     *     
     */
    public void setLogEntries(ArrayOfLogEntry value) {
        this.logEntries = value;
    }

}
