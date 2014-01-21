
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
 *         &lt;element name="ColumnCaption2" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *         &lt;element name="ColumnCaption3" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
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
    "columnCaption2",
    "columnCaption3"
})
public class StatisticDataset {

    @XmlElement(name = "Data")
    protected ArrayOfStatisticData data;
    @XmlElement(name = "Caption")
    protected String caption;
    @XmlElement(name = "ColumnCaption2")
    protected String columnCaption2;
    @XmlElement(name = "ColumnCaption3")
    protected String columnCaption3;

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
     * Ruft den Wert der columnCaption2-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getColumnCaption2() {
        return columnCaption2;
    }

    /**
     * Legt den Wert der columnCaption2-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setColumnCaption2(String value) {
        this.columnCaption2 = value;
    }

    /**
     * Ruft den Wert der columnCaption3-Eigenschaft ab.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getColumnCaption3() {
        return columnCaption3;
    }

    /**
     * Legt den Wert der columnCaption3-Eigenschaft fest.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setColumnCaption3(String value) {
        this.columnCaption3 = value;
    }

}
