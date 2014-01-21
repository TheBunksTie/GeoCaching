
package at.wea5.geocaching.webserviceproxy;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlSchemaType;
import javax.xml.bind.annotation.XmlType;
import javax.xml.datatype.XMLGregorianCalendar;


/**
 * <p>Java-Klasse für LogEntry complex type.
 * 
 * <p>Das folgende Schemafragment gibt den erwarteten Content an, der in dieser Klasse enthalten ist.
 * 
 * <pre>
 * &lt;complexType name="LogEntry">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="Id" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="CacheId" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="CreatorId" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="CreatorName" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="CreationDate" type="{http://www.w3.org/2001/XMLSchema}dateTime"/>
 *         &lt;element name="IsFound" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
 *         &lt;element name="Comment" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "LogEntry", propOrder = {
    "id",
    "cacheId",
    "creatorId",
    "creatorName",
    "creationDate",
    "isFound",
    "comment"
})
public class LogEntry {

    @XmlElement(name = "Id")
    protected int id;
    @XmlElement(name = "CacheId")
    protected int cacheId;
    @XmlElement(name = "CreatorId")
    protected int creatorId;
    @XmlElement(name = "CreatorName")
    protected String creatorName;
    @XmlElement(name = "CreationDate", required = true)
    @XmlSchemaType(name = "dateTime")
    protected XMLGregorianCalendar creationDate;
    @XmlElement(name = "IsFound")
    protected boolean isFound;
    @XmlElement(name = "Comment")
    protected String comment;

    /**
     * Ruft den Wert der id-Eigenschaft ab.
     * 
     */
    public int getId() {
        return id;
    }

    /**
     * Legt den Wert der id-Eigenschaft fest.
     * 
     */
    public void setId(int value) {
        this.id = value;
    }

    /**
     * Ruft den Wert der cacheId-Eigenschaft ab.
     * 
     */
    public int getCacheId() {
        return cacheId;
    }

    /**
     * Legt den Wert der cacheId-Eigenschaft fest.
     * 
     */
    public void setCacheId(int value) {
        this.cacheId = value;
    }

    /**
     * Ruft den Wert der creatorId-Eigenschaft ab.
     * 
     */
    public int getCreatorId() {
        return creatorId;
    }

    /**
     * Legt den Wert der creatorId-Eigenschaft fest.
     * 
     */
    public void setCreatorId(int value) {
        this.creatorId = value;
    }

    /**
     * Ruft den Wert der creatorName-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCreatorName() {
        return creatorName;
    }

    /**
     * Legt den Wert der creatorName-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCreatorName(String value) {
        this.creatorName = value;
    }

    /**
     * Ruft den Wert der creationDate-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link XMLGregorianCalendar }
     *     
     */
    public XMLGregorianCalendar getCreationDate() {
        return creationDate;
    }

    /**
     * Legt den Wert der creationDate-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link XMLGregorianCalendar }
     *     
     */
    public void setCreationDate(XMLGregorianCalendar value) {
        this.creationDate = value;
    }

    /**
     * Ruft den Wert der isFound-Eigenschaft ab.
     * 
     */
    public boolean isIsFound() {
        return isFound;
    }

    /**
     * Legt den Wert der isFound-Eigenschaft fest.
     * 
     */
    public void setIsFound(boolean value) {
        this.isFound = value;
    }

    /**
     * Ruft den Wert der comment-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getComment() {
        return comment;
    }

    /**
     * Legt den Wert der comment-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setComment(String value) {
        this.comment = value;
    }

}
