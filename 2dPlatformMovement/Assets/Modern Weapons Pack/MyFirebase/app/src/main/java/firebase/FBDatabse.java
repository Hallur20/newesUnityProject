package firebase;

import android.util.Log;

import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.ValueEventListener;

import java.util.HashMap;

import entity.User;


public class FBDatabse {

    public FBDatabse(){

  //Database reference
        FirebaseDatabase firebaseDatabase = FirebaseDatabase.getInstance();
        DatabaseReference databaseReference = firebaseDatabase.getReference();

        //insert
        databaseReference.push().setValue("something");
        databaseReference.child("sometext").setValue("something");


        databaseReference.child("sometext").addListenerForSingleValueEvent(new ValueEventListener() {
            @Override
            public void onDataChange(DataSnapshot dataSnapshot) {
                Log.d("sometext","Sometext:" + dataSnapshot.getValue());
            }

            @Override
            public void onCancelled(DatabaseError databaseError) {

            }
        });
        databaseReference.child("sometext").addValueEventListener(new ValueEventListener() {
            @Override
            public void onDataChange(DataSnapshot dataSnapshot) {
                Log.d("sometext","Sometext:" + dataSnapshot.getValue());
            }

            @Override
            public void onCancelled(DatabaseError databaseError) {

            }
        });

        HashMap<String, User> users = new HashMap();
        users.put("Mo", new User("Morshed kayed", 34));
        users.put("Kayed", new User("kayed kayed", 31));
        users.put("Lasse", new User("Lasse andersen", 36));
        users.put("Hallur", new User("Hallur k", 39));
        users.put("Bigs", new User("Bigs Cool", 21));

        databaseReference.child("users").setValue(users);

               User u = new User("Mark And",22);
             databaseReference.child("user").child("Mark").setValue(u);

             databaseReference.child("users").addValueEventListener(new ValueEventListener() {
                 @Override
                 public void onDataChange(DataSnapshot dataSnapshot) {
                     Log.d("user", dataSnapshot.toString());

                     HashMap<String , User> hm = new HashMap();

                     for (DataSnapshot ds : dataSnapshot.getChildren()){
                         Log.d("user", ds.getKey() + " " + ds.getValue());
                         String name = ds.child("name").getValue(String.class);
                         int age = ds.child("age").getValue(int.class);

                         Log.d("users", name+" "+ age);

                         hm.put(ds.getKey(),ds.getValue(User.class));

                     }
                     Log.d("user", "  " + hm.get("Lasse"));
                 }

                 @Override
                 public void onCancelled(DatabaseError databaseError) {

                 }
             });




    }
}