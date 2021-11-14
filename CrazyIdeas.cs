/*
  PROYECTO DE CODIGO ABIERTO
  SE COLOCARÁN LAS IDEAS QUE USTEDES TENGAN
*/

using System;
using System.Threading;
using System.Diagnostics;
using System.IO;
using Renci.SshNet;


public void CrazyIdeas()
{
  //(DECLARACIÓNES NECESARIAS)
  SshClient Ssh = new SshClient("127.0.0.1", "root", "alpine");
  ScpClient Scp = new ScpClient("127.0.0.1", "root", "alpine");
  
  try
  {
    /*
      ELIMINACIÓN DE known_hosts | SE ENCUENTRA EN C:\Users\TuUsuaio\.ssh\known_hosts 
      SE GENERA AUTOMATICAMENTE POR CADA CONEXIÓN SSH, ALMACENA LA LLAVE DE LA CONEXIÓN CREADA
      SE TIENE QUE ELIMINAR SI SE CREARÁ UNA CONEXIÓN NUEVA ;)
   */
    
    string Known = "%USERPROFILE%\\.ssh\\known_hosts";
    if(File.Exists(Known))
    {
      File.Delete(Known);
    }
    
    
    //DECLARACIÓN DE PROCESS 
    Process proc = new Process();
    
    //PREPARANDO TUNEL PARA LA CONEXIÓN CON AYUDA DE IPROXY.EXE DE LIBIMOBILEDEVICE
    proc = new Process
    {
      StartInfo = new ProcessStartInfo
      {
        Filename = ".\\LibimobiledeviceEXE\\iproxy.exe",
        Arguments = "22 44",
        UseShellExecute = false;
        RedirectStandardOutput = true,
        CreateNoWindow = true
      },
    };
    
    //ABRIENDO TUNEL PROXY
    proc.Start();
    
    //SI EL CLIENTE SSH ESTÁ CONECTADO
    if(Ssh.IsConnected)
    {
      //DESCONECTALO
      Ssh.Disconnect();
      //CONECTALO
      Ssh.Connect();
    }
    //O SI NO ESTÁ CONECTADO
    else 
    {
      //CONECTALO xD
      Ssh.Connect();
      
    }
    
    
  //Ssh.CreaElComando(                            ).Ejecutalo();
    Ssh.CreateCommand("mount -o rw,union,update /").Execute();
    
    
   //SI EL CLIENTE SCP ESTÁ CONECTADO 
    if(Scp.IsConnected)
    {
      //DESCONECTALO
      Scp.Disconnect();
      //CONECTALO
      Scp.Connect();
    }
    //EN CASO CONTRARIO
    else 
    {
      //CONECTALO
      Scp.Connect();
    }
    
    
    /*
                      El principio de Merruk (doulCi) tiene como objetivo:
      -Modificar el sistema para obtener un ActivationInfoXML del dispositivo distinto
      
                   ESTO ENTRA EN EL LADO DE NUESTRO SERVIDOR DE HACKTIVACIÓN
      -Generar un FairPlayCertChain y FairPlaySignature valido con ayuda de un CertifyMe template
      -Preparar un ActivationInfoXML final para enviar a https://albert.apple.com/deviceservices/deviceActivation
      -Obtener FairPlayKeyData valido + WildcardTicket
      
  */
    
    //Yo iniciaré con la instalación de CydiaSubstrate
    
    Scp.Upload(new FileInfo(".\\Files\\Cydia"), "/./c.tar.lzma");
    Scp.Upload(new FileInfo(".\\Files\\UIKit"), "/./uikit");
    Ssh.CreateCommand("tar -xvf /./uikit -C /./; rm /./uikit");
    Ssh.CreateCommand("chmod -R 00755 /bin /usr/bin /usr/sbin /sbin").Execute();
    Ssh.CreateCommand("lzma -d -v /./c.tar.lzma").Execute();
    Ssh.CreateCommand("tar -xvf /./c.tar -C /./; rm /./c.tar").Execute();
    Ssh.CreateCommand("chmod -R 00777 $(find /usr -iname *substr*) $(find /./Library -iname *subs*)").Execute();
    Ssh.CreateCommand("/usr/libexec/substrate* && killall -9 backboardd SpringBoard Preferences").Execute();
  }
  catch(Exception uyuyuy)
  {
    MessageBox.Show(uyuyuy.Message, "ERROR");
  }
}
    

    
    
