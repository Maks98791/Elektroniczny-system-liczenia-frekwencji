package com.example.PZ_FTP_TEST;

import android.annotation.SuppressLint;
import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;

import java.io.DataInputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketException;
import java.util.Enumeration;

import android.widget.TextView;

public class MainActivity extends AppCompatActivity {

    TextView info, info2, msg;
    String message = "";
    ServerSocket serverSocket;
    public static String SERVER_IP = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        info = (TextView) findViewById(R.id.info);
        info2 = (TextView) findViewById(R.id.info2);
        msg = (TextView) findViewById(R.id.msg);

        //info2.setText("Port: 8080");
        //try {
            info.setText("AdresIP: " + getIpAddress());
        //} catch (UnknownHostException e) {
        //    e.printStackTrace();
        //}


        Thread socketServerThread = new Thread(new SocketServerThread());
        socketServerThread.start();
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();

        if (serverSocket != null) {
            try {
                serverSocket.close();
            } catch (IOException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            }
        }
    }

   private class SocketServerThread extends Thread {

        static final int SocketServerPORT = 8080;
        int count = 0;

        @Override
        public void run() {
            try {
                serverSocket = new ServerSocket( SocketServerPORT);

                MainActivity.this.runOnUiThread(new Runnable() {

                    @SuppressLint("SetTextI18n")
                    @Override
                    public void run() {
                        info2.setText("Port: " + serverSocket.getLocalPort());
                    }
                });

                while (true) {
                    Socket socket = serverSocket.accept();
                    count++;
                    message += "#" + count + " from " + socket.getInetAddress()
                            + ":" + socket.getPort() + "\n";

                    MainActivity.this.runOnUiThread(new Runnable() {

                        @Override
                        public void run() {
                            msg.setText(message);
                        }
                    });

                    SocketServerReplyThread socketServerReplyThread = new SocketServerReplyThread(
                            socket, count);
                    socketServerReplyThread.run();

                }
            } catch (IOException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            }
        }

    }

    private class SocketServerReplyThread extends Thread {
        private Socket hostThreadSocket;
        int cnt;
        SocketServerReplyThread(Socket socket, int c) {
            hostThreadSocket = socket;
            cnt = c;
        }
        @Override
        public void run() {
            OutputStream outputStream;
            String msgReply = "\n\nOdebrano pakiet nr" + cnt;
            try {
                DataInputStream in = new DataInputStream(hostThreadSocket.getInputStream());
                StringBuilder g = new StringBuilder();
                while(in.available()>0) {
                    int temp = in.read();
                    g.append(String.valueOf((char) temp));
                }
                message += "Odebrana wiadomość: " + g + "\n\n";
                in.close();
                MainActivity.this.runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        msg.append(message);
                    }
                });
            } catch (IOException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
                message += "Nie można odczytać wiadomości! " + e.toString() + "\n";
            }
            MainActivity.this.runOnUiThread(new Runnable() {

                @Override
                public void run() {
                    msg.setText(message);
                }
            });
        }
    }


    //outputStream = hostThreadSocket.getOutputStream();
    //PrintStream printStream = new PrintStream(outputStream);
    //printStream.print(msgReply);

    //message += "Odpowiedź: " + msgReply + "\n";



    private String getIpAddress() {
        String ip = "Cannot get local ip - check your internet connection ";
        try {
            Enumeration<NetworkInterface> enumNetworkInterfaces = NetworkInterface
                    .getNetworkInterfaces();
            while (enumNetworkInterfaces.hasMoreElements()) {
                NetworkInterface networkInterface = enumNetworkInterfaces
                        .nextElement();
                Enumeration<InetAddress> enumInetAddress = networkInterface
                        .getInetAddresses();
                while (enumInetAddress.hasMoreElements()) {
                    InetAddress inetAddress = enumInetAddress.nextElement();

                    if (inetAddress.isSiteLocalAddress()) {
                        ip = "AdresIP: " + inetAddress.getHostAddress() + "\n";
                    }

                }

            }

        } catch (SocketException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
            ip += "Cannot get local ip - check your internet connection " + e.toString() + "\n";
        }

        return ip;
    }
}
