﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTibiaX.IPChanger.Data {

    [Serializable]
    public class Address {

        public Version Version { get; set; }

        public uint RSAKey { get; set; }

        public uint MultiClient { get; set; }

        public uint LoginServer { get; set; }

        public uint ptrFrameRateBegin { get; set; }

        public uint FrameRateCurrentOffset { get; set; }

        public uint FrameRateLimitOffset { get; set; }

        public bool Older { get { return Version.GetHashCode() < Version.v800.GetHashCode(); } }

        public Address(Version version) {
            Version = version;
            switch (version) {

                #region "[rgn] Address Parametrization "
                case Version.v760:
                    MultiClient = 0x0;
                    LoginServer = 0x5EFBC0;
                    RSAKey = 0x0;
                    ptrFrameRateBegin = 0x0;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v792:
                    MultiClient = 0x0;
                    LoginServer = 0x755E88;
                    RSAKey = 0x58D620;
                    ptrFrameRateBegin = 0x75DF24;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v800:
                    MultiClient = 0x4F6245;
                    LoginServer = 0x75EAE8;
                    RSAKey = 0x593610;
                    ptrFrameRateBegin = 0x76793C;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v810:
                    MultiClient = 0x4F8965;
                    LoginServer = 0x763BB8;
                    RSAKey = 0x597610;
                    ptrFrameRateBegin = 0x76CE0C;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v811:
                    MultiClient = 0x4F8965;
                    LoginServer = 0x763BB8;
                    RSAKey = 0x597610;
                    ptrFrameRateBegin = 0x76CE0C;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v820:
                    MultiClient = 0x500D05;
                    LoginServer = 0x771CF0;
                    RSAKey = 0x5A3610;
                    ptrFrameRateBegin = 0x77AF3C;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v821:
                    MultiClient = 0x502B95;
                    LoginServer = 0x774CF0;
                    RSAKey = 0x5A5610;
                    ptrFrameRateBegin = 0x77DF3C;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v822:
                    MultiClient = 0x502BB5;
                    LoginServer = 0x776CF0;
                    RSAKey = 0x5A7610;
                    ptrFrameRateBegin = 0x77FF3C;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v830:
                    MultiClient = 0x505515;
                    LoginServer = 0x77AD88;
                    RSAKey = 0X5AA610;
                    ptrFrameRateBegin = 0x783FF4;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v831:
                    MultiClient = 0x5058B5;
                    LoginServer = 0x77AD88;
                    RSAKey = 0X5AA610;
                    ptrFrameRateBegin = 0x783FF4;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v84:
                    MultiClient = 0x505945;
                    LoginServer = 0x77FC48;
                    RSAKey = 0X5AB610;
                    ptrFrameRateBegin = 0x788EB4;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v841:
                    MultiClient = 0x5061E5;
                    LoginServer = 0x780CD0;
                    RSAKey = 0x5AB610;
                    ptrFrameRateBegin = 0x789F3C;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v842:
                    MultiClient = 0x505F15;
                    LoginServer = 0x785D30;
                    RSAKey = 0x5AF610;
                    ptrFrameRateBegin = 0x78EF9C;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v850:
                    MultiClient = 0x5067B5;
                    LoginServer = 0x786E70;
                    RSAKey = 0x5B0610;
                    ptrFrameRateBegin = 0x7900DC;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v854:
                    MultiClient = 0x5070D5;
                    LoginServer = 0x78A728;
                    RSAKey = 0x5B2610;
                    ptrFrameRateBegin = 0x79399C;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                case Version.v855:
                    MultiClient = 0x50B895;
                    LoginServer = 0x791D20;
                    RSAKey = 0x5B7610;
                    ptrFrameRateBegin = 0x79AF9C;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;
					
				 case Version.v857:
                    MultiClient = 0x50BB45;
                    LoginServer = 0x7947F0;
                    RSAKey = 0x5B8980;
                    ptrFrameRateBegin = 0x79DA6C;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;

                 case Version.v860:
                    MultiClient = 0x50BCC5;
                    LoginServer = 0x7947F8;
                    RSAKey = 0x5B8980;
                    ptrFrameRateBegin = 0x79DA74;
                    FrameRateCurrentOffset = 0x60;
                    FrameRateLimitOffset = 0x58;
                    break;
                #endregion

                default: throw new ArgumentException("Invalid Version of Memory Addressess!");

            }
        }
    }

}
