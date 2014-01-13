package at.wea5.geocaching.domainmodel;

/**
 * reduced variant of domain class user from -net project.
 * only relevant attributes from original user are stored
 *
 */
public class User {

//------------------------------------ constructor ------------------------------------

    public User(int id, String name, String password, String email, GeoPosition position) {
        this.id = id;
        this.name = name;
        this.password = password;
        this.email = email;
        this.position = position;
    }
    
    
    public User(at.wea5.geocaching.webserviceproxy.User user) {
        this.id = user.getId();
        this.name = user.getName();
        this.password = user.getPassword();        
        this.email = user.getEmail();
        this.position = new GeoPosition(user.getPosition().getLatitude(), user.getPosition().getLongitude());         
    }
    
//-------------------------------------- public ---------------------------------------
    
    public int getId() {
        return id;
    }
    
    public void setId(int id) {
        this.id = id;
    }
    
    public String getName() {
        return name;
    }
    
    public void setName(String name) {
        this.name = name;
    }
    
    public String getPassword() {
        return password;
    }
    
    public void setPassword(String password) {
        this.password = password;
    }
    
    public String getEmail() {
        return email;
    }
    
    public void setEmail(String email) {
        this.email = email;
    }
    
    public GeoPosition getPosition() {
        return position;
    }
    
    public void setPosition(GeoPosition position) {
        this.position = position;
    }
     
//------------------------------------- private ---------------------------------------

//-------------------------------------- members --------------------------------------
    
    private int id;
    private String name;
    private String password;
    private String email;
    private GeoPosition position;    
}
