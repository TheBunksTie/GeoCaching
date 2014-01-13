
package at.wea5.geocaching.webserviceproxy;

import javax.xml.bind.annotation.XmlEnum;
import javax.xml.bind.annotation.XmlEnumValue;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java-Klasse für FilterOperation.
 * 
 * <p>Das folgende Schemafragment gibt den erwarteten Content an, der in dieser Klasse enthalten ist.
 * <p>
 * <pre>
 * &lt;simpleType name="FilterOperation">
 *   &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *     &lt;enumeration value="Exact"/>
 *     &lt;enumeration value="Above"/>
 *     &lt;enumeration value="AboveEquals"/>
 *     &lt;enumeration value="Below"/>
 *     &lt;enumeration value="BelowEquals"/>
 *   &lt;/restriction>
 * &lt;/simpleType>
 * </pre>
 * 
 */
@XmlType(name = "FilterOperation")
@XmlEnum
public enum FilterOperation {

    @XmlEnumValue("Exact")
    EXACT("Exact"),
    @XmlEnumValue("Above")
    ABOVE("Above"),
    @XmlEnumValue("AboveEquals")
    ABOVE_EQUALS("AboveEquals"),
    @XmlEnumValue("Below")
    BELOW("Below"),
    @XmlEnumValue("BelowEquals")
    BELOW_EQUALS("BelowEquals");
    private final String value;

    FilterOperation(String v) {
        value = v;
    }

    public String value() {
        return value;
    }

    public static FilterOperation fromValue(String v) {
        for (FilterOperation c: FilterOperation.values()) {
            if (c.value.equals(v)) {
                return c;
            }
        }
        throw new IllegalArgumentException(v);
    }

}
