﻿using System.Collections.Generic;
using AirplaneSimulationTrajectory.Constants;
using AirplaneSimulationTrajectory.Helpers;

namespace AirplaneSimulationTrajectory.Model
{
    public static class TrajectoryData
    {
        public static CustomLinkedList<RoutePointModel> GetRoute()
        {
            return new CustomLinkedList<RoutePointModel>(new List<RoutePointModel>
            {
                new RoutePointModel(45.046154, 7.728707, AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(45.5145797732176, 8.16653194181995,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(46.4537328433948, 8.39189677298887,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(46.4537328433948, 8.39189677298887,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(46.924413032357, 8.62176382866551,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(47.3957960305745, 8.85629212932187,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(47.867855879646, 9.09564797533506,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(48.3405655678913, 9.34000535386099,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(48.8138969740161, 9.58954637207546,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(49.2878208068418, 9.84446171867873,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(49.7623065407817, 10.1049511557044,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(50.2373223467188, 10.3712240428261,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(50.7128350179069, 10.6434998965229,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(51.1888098904841, 10.922008986641,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(51.6652107581501, 11.2069929730778,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(52.141999780517, 11.4987055855167,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(52.6191373845984, 11.7974133493578,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(53.0965821588519, 12.1033963612132,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(53.5742907391365, 12.4169491175802,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(54.0522176858842, 12.7383814005564,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(54.5303153517232, 13.068019224727,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(55.0085337387136, 13.4062058496279,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(55.4868203442804, 13.7533028624753,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(55.9651199948414, 14.1096913361395,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(56.4433746660306, 14.4757730676368,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(56.9215232883142, 14.8519719026987,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(57.3995015366839, 15.2387351522654,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(57.8772416029829, 15.6365351070089,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(58.3546719492882, 16.0458706562298,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(58.8317170406198, 16.4672690176665,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(59.3082970550889, 16.9012875848882,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(59.7843275694189, 17.348515898996,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(60.2597192175847, 17.8095777512992,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(60.7343773201094, 18.2851334234319,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(61.208201481341, 18.7758820709816,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(61.6810851517932, 19.2825642560717,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(62.1529151523917, 19.8059646333927,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(62.623571157204, 20.3469147928462,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(63.0929251309636, 20.9062962601358,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(63.5608407174245, 21.4850436542012,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(64.0271725743087, 22.084147997181,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(64.4917656503394, 22.704660168439,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(64.9544543996109, 23.3476944888731,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(65.4150619283256, 24.0144324149778,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(65.8733990687704, 24.7061263136559,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(66.329263375318, 25.4241032781849,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(66.7824380372605, 26.169768932622,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(67.2326907034603, 26.9446111557787,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(67.6797722141739, 27.7502036361369,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(68.1234152360531, 28.5882091450841,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(68.5633327973154, 29.4603823869223,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(68.9992167215209, 30.3685722495118,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(69.4307359604011, 31.3147232384124,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(69.85753482892, 32.3008758293053,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(70.2792311493614, 33.3291654177571,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(70.695414315952, 34.4018194817812,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(71.1056432975527, 35.5211525013337,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(71.5094446035449, 36.6895581007758,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(71.9063102474472, 37.9094977973966,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(72.2956957542892, 39.1834856547747,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(72.6770182715353, 40.5140680595246,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(73.0496548595471, 41.903797771918,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(73.4129410561743, 43.3552013563506,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(73.7661698308456, 44.8707390918888,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(74.108591065929, 46.4527565157043,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(74.4394117261292, 48.1034268868778,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(74.7577968986727, 49.8246841021102,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(75.0628719056766, 51.6181459771356,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(75.353725702244, 53.4850283548806,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(75.6294157754983, 55.4260512327954,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(75.8889747462974, 57.4413390209901,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(76.1314188417135, 59.530318128377,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(76.3557583478137, 61.6916162689904,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(76.5610100652965, 63.9229690837177,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(76.7462116740788, 66.2211407344836,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(76.9104377697014, 68.5818658566558,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(77.0528171721716, 70.9998204358283,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(77.1725509399522, 73.4686286029247,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(77.2689303669437, 75.9809108712799,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(77.3413541207253, 78.5283769372935,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(77.3893436186442, 81.1019629547428,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(77.4125557532004, 83.6920094686286,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(77.4107921796439, 86.288472406562,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(77.3840045646425, 88.8811562081788,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(77.3322954495685, 91.4599558420812,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(77.2559146776273, 94.0150935006172,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(77.155251635463, 96.5373363387792,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(77.0308238307495, 99.018183636226,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(76.88326253702, 101.450014874788,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(76.7132963659665, 103.826193944383,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(76.5217336694075, 106.141128477882,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(76.3094446347159, 108.390286691523,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(76.0773438351021, 110.570176732816,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(75.8263738515013, 112.678295246292,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(75.5574904185152, 114.713052656773,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(75.2716493827702, 116.673682661957,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(74.9697956131692, 118.560142815611,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(74.6528538781844, 120.373012085595,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(74.3217216097146, 122.113390087678,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(73.9772634061011, 123.782801487619,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(73.6203070858228, 125.383107943711,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(73.2516410837972, 126.91642899743,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(72.8720129791446, 128.385072540463,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(72.4821289519954, 129.791474893853,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(72.0826539832624, 131.138150113369,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(71.674212631897, 32.427647858867,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(71.2573902464592, 133.662519005755,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(70.8327344900379, 134.845288105711,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(70.4007570784971, 135.978431796833,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(69.9619356509888, 137.064362299656,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(69.516715708349, 138.105415198857,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(69.0655125692959, 139.103840788436,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius),
                new RoutePointModel(68.6087133063707, 140.061798342097,
                    AppConstants.RadiusDelta + AppConstants.EarthRadius)
            });
        }
    }
}