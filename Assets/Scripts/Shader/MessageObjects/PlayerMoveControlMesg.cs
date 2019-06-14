//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年06月14日-17:08
//Assembly-CSharp

using MessagePack;

namespace Shader.MessageObjects
{
    [MessagePackObject()]
    public struct Vect4
    {
        [Key(0)]
        public float x;
        [Key(1)]
        public float y;
        [Key(2)]
        public float z;
        [Key(3)]
        public float w;

        public Vect4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }

    [MessagePackObject()]
    public struct Vect3
    {
        [Key(0)]
        public float x;
        [Key(1)]
        public float y;
        [Key(2)]
        public float z;

        public Vect3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
    
    
    [MessagePackObject()]
    public class PlayerMoveControlMesg
    {
        /// <summary>
        /// 横轴
        /// </summary>
        [Key(0)]
        public Vect3 Move;

        /// <summary>
        /// 旋转
        /// </summary>
        [Key(1)]
        public Vect4 quaternion;

        /// <summary>
        /// 玩家名
        /// </summary>
        [Key(2)]
        public string PlayerName;

        [Key(3)]
        public bool Crouch;

        [Key(4)]
        public bool Jump;
        
        [Key(5)]
        public Vect3 Pos;

        public PlayerMoveControlMesg(Vect3 move, Vect4 quaternion, string playerName, bool crouch, bool jump, Vect3 pos)
        {
            Move = move;
            this.quaternion = quaternion;
            PlayerName = playerName;
            Crouch = crouch;
            Jump = jump;
            Pos = pos;
        }
    }
}