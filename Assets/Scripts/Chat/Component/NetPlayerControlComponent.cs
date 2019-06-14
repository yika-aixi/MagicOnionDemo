//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-18:25
//Assembly-CSharp

using System;
using MagicOnion.Client;
using MagicOnionTestService.LobbyMessageTest.PlayerControlSyn;
using Shader.MessageObjects;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Chat.Component
{
    public class NetPlayerControlComponent:MonoBehaviour,IPlayerControlSynReceiver
    {
        public static NetPlayerControlComponent Instance;
        private IPlayerControlSynHub _controlSyn;
        void Awake()
        {
            Instance = this;
            
            RoomComponent.JoinRoomEve += RoomComponentOnJoinRoomEve;
            RoomComponent.LeaveRoomEve += RoomComponentOnLeaveRoomEve;
        }

        private void RoomComponentOnLeaveRoomEve()
        {
            try
            {
                _controlSyn.DisposeAsync();
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        private void OnDestroy()
        {
            RoomComponentOnLeaveRoomEve();
        }

        private string _playerName;
        private void RoomComponentOnJoinRoomEve()
        {
            _controlSyn =  StreamingHubClient.Connect<IPlayerControlSynHub, IPlayerControlSynReceiver>(MagicOnionManager.Instance.Channel, this);
            _waitDisConnect();
            _playerName = LocalPlayer.PlayerName;
            _controlSyn.Init(new PlayerControlInitMesg(LocalPlayer.RoomName,LocalPlayer.PlayerName));
            _mesg = new PlayerMoveControlMesg(Vector3.zero.UVect3ToVect3(), Vector4.zero.UVect4ToVect4(),LocalPlayer.PlayerName,false,false,Vector3.zero.UVect3ToVect3());

            ThirdPersonUserControl.OnMove = (pos,vector3, quaternion, arg3, arg4) =>
            {
                pos.UVect3ToVect3(ref _mesg.Pos);
                vector3.UVect3ToVect3(ref _mesg.Move);
                quaternion.QuaternionToVect4(ref _mesg.quaternion);
                _mesg.Crouch = arg3;
                _mesg.Jump = arg4;
                

//                _mesg = new PlayerMoveControlMesg(vector3.UVect3ToVect3(), quaternion.QuaternionToVect4(),LocalPlayer.PlayerName,arg3,arg4);
                
                Move(_mesg);
            };
        }
        
        private async void _waitDisConnect()
        {
            try
            {
                await _controlSyn.WaitForDisconnect();
            }
            catch (Exception e)
            {
                RoomComponentOnLeaveRoomEve();
            }
        }

        private PlayerMoveControlMesg _mesg;
        
        public void OnMove(PlayerMoveControlMesg mesg)
        {
            if (LocalPlayer.PlayerName.Equals(mesg.PlayerName))
            {
                return;   
            }

            try
            {
                var player = PlayerManager.Instance.GetPlayer(mesg.PlayerName);

                if (!player)
                {
                    player = PlayerManager.Instance.CreatePlayer(false, mesg.PlayerName);
                    player.transform.position = mesg.Pos.Vect3ToUVect3();
                }
            
                player.transform.rotation = mesg.quaternion.Vect4ToQuaternion();
            
                var con = player.GetComponent<ThirdPersonCharacter>();
            
                con.Move(mesg.Move.Vect3ToUVect3(),mesg.Crouch,mesg.Jump);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public async void Move(PlayerMoveControlMesg mesg)
        {
            await _controlSyn.Move(mesg);
        }
    }

    static class VToV
    {
        public static Vector3 Vect3ToUVect3(this Vect3 self)
        {
            return new Vector3(self.x,self.y,self.z);
        }
        
        public static Vector4 Vect4ToUVect4(this Vect4 self)
        {
            return new Vector4(self.x,self.y,self.z,self.w);
        }
        
        public static Quaternion Vect4ToQuaternion(this Vect4 self)
        {
            return new Quaternion(self.x,self.y,self.z,self.w);
        }
        
        public static Vect3 UVect3ToVect3(this Vector3 self)
        {
            return new Vect3(self.x,self.y,self.z);
        }

        public static void UVect3ToVect3(this Vector3 self, ref Vect3 vect)
        {
            vect.x = self.x;
            vect.y = self.y;
            vect.z = self.z;
        }
        
        public static Vect4 UVect4ToVect4(this Vector4 self)
        {
            return new Vect4(self.x,self.y,self.z,self.w);
        }
        
        public static void UVect4ToVect4(this Vector4 self, ref Vect4 vect)
        {
            vect.x = self.x;
            vect.y = self.y;
            vect.z = self.z;
            vect.w = self.w;
        }
        
        public static Vect4 QuaternionToVect4(this Quaternion self)
        {
            return new Vect4(self.x,self.y,self.z,self.w);
        }
        
        public static void QuaternionToVect4(this Quaternion self, ref Vect4 vect)
        {
            vect.x = self.x;
            vect.y = self.y;
            vect.z = self.z;
            vect.w = self.w;
        }
    }
}