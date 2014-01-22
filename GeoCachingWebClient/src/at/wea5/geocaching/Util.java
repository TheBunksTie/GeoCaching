package at.wea5.geocaching;

import java.util.Date;
import java.util.GregorianCalendar;

import javax.xml.datatype.DatatypeConfigurationException;
import javax.xml.datatype.DatatypeFactory;
import javax.xml.datatype.XMLGregorianCalendar;

public class Util {

//------------------------------------ constructor ------------------------------------

//-------------------------------------- public ---------------------------------------
    
    public static final XMLGregorianCalendar convertToXML(Date date) throws DatatypeConfigurationException {
        Util.calendar.setTime(date);                     
        return DatatypeFactory.newInstance().newXMLGregorianCalendar(calendar);
    }
    
    
//------------------------------------- private ---------------------------------------

//-------------------------------------- members --------------------------------------
    
    private static final GregorianCalendar calendar = new GregorianCalendar();
}
