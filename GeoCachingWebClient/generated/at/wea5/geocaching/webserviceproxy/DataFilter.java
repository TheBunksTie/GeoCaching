
package at.wea5.geocaching.webserviceproxy;

import java.util.Date;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlSchemaType;
import javax.xml.bind.annotation.XmlType;
import javax.xml.datatype.DatatypeConfigurationException;
import javax.xml.datatype.XMLGregorianCalendar;

import at.wea5.geocaching.Util;


/**
 * <p>Java-Klasse f�r DataFilter complex type.
 * 
 * <p>Das folgende Schemafragment gibt den erwarteten Content an, der in dieser Klasse enthalten ist.
 * 
 * <pre>
 * &lt;complexType name="DataFilter">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="CacheName" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="FromPosition" type="{http://GeoCaching.Services/}GeoPosition"/>
 *         &lt;element name="ToPosition" type="{http://GeoCaching.Services/}GeoPosition"/>
 *         &lt;element name="FromDate" type="{http://www.w3.org/2001/XMLSchema}dateTime"/>
 *         &lt;element name="ToDate" type="{http://www.w3.org/2001/XMLSchema}dateTime"/>
 *         &lt;element name="FromCacheSize" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="ToCacheSize" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="FromCacheDifficulty" type="{http://www.w3.org/2001/XMLSchema}double"/>
 *         &lt;element name="ToCacheDifficulty" type="{http://www.w3.org/2001/XMLSchema}double"/>
 *         &lt;element name="FromTerrainDifficulty" type="{http://www.w3.org/2001/XMLSchema}double"/>
 *         &lt;element name="ToTerrainDifficulty" type="{http://www.w3.org/2001/XMLSchema}double"/>
 *         &lt;element name="Limit" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "DataFilter", propOrder = {
    "cacheName",
    "fromPosition",
    "toPosition",
    "fromDate",
    "toDate",
    "fromCacheSize",
    "toCacheSize",
    "fromCacheDifficulty",
    "toCacheDifficulty",
    "fromTerrainDifficulty",
    "toTerrainDifficulty",
    "limit"
})
public class DataFilter {

    @XmlElement(name = "CacheName")
    protected String cacheName;
    @XmlElement(name = "FromPosition", required = true)
    protected GeoPosition fromPosition;
    @XmlElement(name = "ToPosition", required = true)
    protected GeoPosition toPosition;
    @XmlElement(name = "FromDate", required = true)
    @XmlSchemaType(name = "dateTime")
    protected XMLGregorianCalendar fromDate;
    @XmlElement(name = "ToDate", required = true)
    @XmlSchemaType(name = "dateTime")
    protected XMLGregorianCalendar toDate;
    @XmlElement(name = "FromCacheSize")
    protected int fromCacheSize;
    @XmlElement(name = "ToCacheSize")
    protected int toCacheSize;
    @XmlElement(name = "FromCacheDifficulty")
    protected double fromCacheDifficulty;
    @XmlElement(name = "ToCacheDifficulty")
    protected double toCacheDifficulty;
    @XmlElement(name = "FromTerrainDifficulty")
    protected double fromTerrainDifficulty;
    @XmlElement(name = "ToTerrainDifficulty")
    protected double toTerrainDifficulty;
    @XmlElement(name = "Limit")
    protected int limit;

    /**
     * Ruft den Wert der cacheName-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCacheName() {
        return cacheName;
    }

    /**
     * Legt den Wert der cacheName-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCacheName(String value) {
        this.cacheName = value;
    }

    /**
     * Ruft den Wert der fromPosition-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link GeoPosition }
     *     
     */
    public GeoPosition getFromPosition() {
        return fromPosition;
    }

    /**
     * Legt den Wert der fromPosition-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link GeoPosition }
     *     
     */
    public void setFromPosition(GeoPosition value) {
        this.fromPosition = value;
    }

    /**
     * Ruft den Wert der toPosition-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link GeoPosition }
     *     
     */
    public GeoPosition getToPosition() {
        return toPosition;
    }

    /**
     * Legt den Wert der toPosition-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link GeoPosition }
     *     
     */
    public void setToPosition(GeoPosition value) {
        this.toPosition = value;
    }

    /**
     * Ruft den Wert der fromDate-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link XMLGregorianCalendar }
     *     
     */
    public XMLGregorianCalendar getFromDate() {
        return fromDate;
    }

    /**
     * Legt den Wert der fromDate-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link XMLGregorianCalendar }
     *     
     */
    public void setFromDate(XMLGregorianCalendar value) {
        this.fromDate = value;
    }

    /**
     * Ruft den Wert der toDate-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link XMLGregorianCalendar }
     *     
     */
    public XMLGregorianCalendar getToDate() {
        return toDate;
    }

    /**
     * Legt den Wert der toDate-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link XMLGregorianCalendar }
     *     
     */
    public void setToDate(XMLGregorianCalendar value) {
        this.toDate = value;
    }

// -------------------- manually added getter/setter for better data-format handling

public Date getFromJavaDate() {        
    return fromDate.toGregorianCalendar().getTime();
}

public void setFromJavaDate(Date value) throws DatatypeConfigurationException {        
    this.fromDate = Util.convertToXML(value);
}

public Date getToJavaDate() {
    return toDate.toGregorianCalendar().getTime();
}

public void setToJavaDate(Date value) throws DatatypeConfigurationException {
    this.toDate = Util.convertToXML(value);
}
        
    // -------------------------------------------------------------------------------------------
    
    
    /**
     * Ruft den Wert der fromCacheSize-Eigenschaft ab.
     * 
     */
    public int getFromCacheSize() {
        return fromCacheSize;
    }

    /**
     * Legt den Wert der fromCacheSize-Eigenschaft fest.
     * 
     */
    public void setFromCacheSize(int value) {
        this.fromCacheSize = value;
    }

    /**
     * Ruft den Wert der toCacheSize-Eigenschaft ab.
     * 
     */
    public int getToCacheSize() {
        return toCacheSize;
    }

    /**
     * Legt den Wert der toCacheSize-Eigenschaft fest.
     * 
     */
    public void setToCacheSize(int value) {
        this.toCacheSize = value;
    }

    /**
     * Ruft den Wert der fromCacheDifficulty-Eigenschaft ab.
     * 
     */
    public double getFromCacheDifficulty() {
        return fromCacheDifficulty;
    }

    /**
     * Legt den Wert der fromCacheDifficulty-Eigenschaft fest.
     * 
     */
    public void setFromCacheDifficulty(double value) {
        this.fromCacheDifficulty = value;
    }

    /**
     * Ruft den Wert der toCacheDifficulty-Eigenschaft ab.
     * 
     */
    public double getToCacheDifficulty() {
        return toCacheDifficulty;
    }

    /**
     * Legt den Wert der toCacheDifficulty-Eigenschaft fest.
     * 
     */
    public void setToCacheDifficulty(double value) {
        this.toCacheDifficulty = value;
    }

    /**
     * Ruft den Wert der fromTerrainDifficulty-Eigenschaft ab.
     * 
     */
    public double getFromTerrainDifficulty() {
        return fromTerrainDifficulty;
    }

    /**
     * Legt den Wert der fromTerrainDifficulty-Eigenschaft fest.
     * 
     */
    public void setFromTerrainDifficulty(double value) {
        this.fromTerrainDifficulty = value;
    }

    /**
     * Ruft den Wert der toTerrainDifficulty-Eigenschaft ab.
     * 
     */
    public double getToTerrainDifficulty() {
        return toTerrainDifficulty;
    }

    /**
     * Legt den Wert der toTerrainDifficulty-Eigenschaft fest.
     * 
     */
    public void setToTerrainDifficulty(double value) {
        this.toTerrainDifficulty = value;
    }

    /**
     * Ruft den Wert der limit-Eigenschaft ab.
     * 
     */
    public int getLimit() {
        return limit;
    }

    /**
     * Legt den Wert der limit-Eigenschaft fest.
     * 
     */
    public void setLimit(int value) {
        this.limit = value;
    }

}
