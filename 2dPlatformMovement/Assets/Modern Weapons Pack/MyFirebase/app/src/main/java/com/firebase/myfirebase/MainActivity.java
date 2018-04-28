package com.firebase.myfirebase;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import firebase.FBDatabse;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        new FBDatabse();
    }
}
