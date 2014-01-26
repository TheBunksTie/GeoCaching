
package at.wea5.geocaching.webserviceproxy;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java-Klasse für StatisticDataset complex type.
 * 
 * <p>Das folgende Schemafragment gibt den erwarteten Content an, der in dieser Klasse enthalten ist.
 * 
 * <pre>
 * &lt;complexType name="StatisticDataset">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="Data" type="{http://GeoCaching.Services/}ArrayOfStatisticData" minOccurs="0"/>
 *         &lt;element name="Caption" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="NrColumnRequired" type="{http://www.w3.org/2001/XMLSchema}boolean"/>
 *         &lt;element name="Column2Caption" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="Column3Caption" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "StatisticDataset", propOrder = {
    "data",
    "caption",
    "nrColumnRequired",
    "column2Caption",
    "column3Caption"
})
public class StatisticDataset {

    @XmlElement(name = "Data")
    protected ArrayOfStatisticData data;
    @XmlElement(name = "Caption")
    protected String caption;
    @XmlElement(name = "NrColumnRequired")
    protected boolean nrColumnRequired;
    @XmlElement(name = "Column2Caption")
    protected String column2Caption;
    @XmlElement(name = "Column3Caption")
    protected String column3Caption;

    /**
     * Ruft den Wert der data-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link ArrayOfStatisticData }
     *     
     */
    public ArrayOfStatisticData getData() {
        return data;
    }

    /**
     * Legt den Wert der data-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link ArrayOfStatisticData }
     *     
     */
    public void setData(ArrayOfStatisticData value) {
        this.data = value;
    }

    /**
     * Ruft den Wert der caption-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCaption() {
        return caption;
    }

    /**
     * Legt den Wert der caption-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCaption(String value) {
        this.caption = value;
    }

    /**
     * Ruft den Wert der nrColumnRequired-Eigenschaft ab.
     * 
     */
    public boolean isNrColumnRequired() {
        return nrColumnRequired;
    }

    /**
     * Legt den Wert der nrColumnRequired-Eigenschaft fest.
     * 
     */
    public void setNrColumnRequired(boolean value) {
        this.nrColumnRequired = value;
    }

    /**
     * Ruft den Wert der column2Caption-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getColumn2Caption() {
        return column2Caption;
    }

    /**
     * Legt den Wert der column2Caption-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setColumn2Caption(String value) {
        this.column2Caption = value;
    }

    /**
     * Ruft den Wert der column3Caption-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getColumn3Caption() {
        return column3Caption;
    }

    /**
     * Legt den Wert der column3Caption-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setColumn3Caption(String value) {
        this.column3Caption = value;
    }

}
