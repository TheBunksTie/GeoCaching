
package at.wea5.geocaching.webserviceproxy;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlSchemaType;
import javax.xml.bind.annotation.XmlType;
import javax.xml.datatype.XMLGregorianCalendar;


/**
 * <p>Java-Klasse für Rating complex type.
 * 
 * <p>Das folgende Schemafragment gibt den erwarteten Content an, der in dieser Klasse enthalten ist.
 * 
 * <pre>
 * &lt;complexType name="Rating">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="Id" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="CacheId" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="CreatorId" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *         &lt;element name="CreationDate" type="{http://www.w3.org/2001/XMLSchema}dateTime"/>
 *         &lt;element name="Grade" type="{http://www.w3.org/2001/XMLSchema}int"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "Rating", propOrder = {
    "id",
    "cacheId",
    "creatorId",
    "creationDate",
    "grade"
})
public class Rating {

    @XmlElement(name = "Id")
    protected int id;
    @XmlElement(name = "CacheId")
    protected int cacheId;
    @XmlElement(name = "CreatorId")
    protected int creatorId;
    @XmlElement(name = "CreationDate", required = true)
    @XmlSchemaType(name = "dateTime")
    protected XMLGregorianCalendar creationDate;
    @XmlElement(name = "Grade")
    protected int grade;

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
     * Ruft den Wert der grade-Eigenschaft ab.
     * 
     */
    public int getGrade() {
        return grade;
    }

    /**
     * Legt den Wert der grade-Eigenschaft fest.
     * 
     */
    public void setGrade(int value) {
        this.grade = value;
    }

}
