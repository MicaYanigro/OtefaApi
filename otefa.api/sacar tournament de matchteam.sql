﻿IF object_id(N'[dbo].[FK_dbo.MatchTeams_dbo.Tournaments_Tournament_Id]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[MatchTeams] DROP CONSTRAINT [FK_dbo.MatchTeams_dbo.Tournaments_Tournament_Id]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_Tournament_Id' AND object_id = object_id(N'[dbo].[MatchTeams]', N'U'))
    DROP INDEX [IX_Tournament_Id] ON [dbo].[MatchTeams]
DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.MatchTeams')
AND col_name(parent_object_id, parent_column_id) = 'Tournament_Id';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[MatchTeams] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[MatchTeams] DROP COLUMN [Tournament_Id]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201710130115224_AutomaticMigration', N'Otefa.Infrastructure.Persistence.Migrations.Configuration',  0x1F8B0800000000000400ED1DDB6EDCB8F5BD40FF61304F6D91F5D8C9662F81BD8BC48E778D26B16167B7ED5320CFD013A11AC92B69921845BFAC0FFDA4FE424989A2783BBC489446F61A01028F481E9E1B0F0F0F79C8FFFDE7BF873F7ED924B34F282FE22C3D9A1FECEDCF67285D66AB385D1FCDB7E5CD57DFCD7FFCE18F7F387CBDDA7C99FDDAD47B46EAE196697134FF5896B72F168B62F9116DA2626F132FF3ACC86ECABD65B65944AB6CF1747FFFFBC5C1C1026110730C6B363BBCDCA665BC41D50FFCF3384B97E8B6DC46C9DB6C8592827EC7255715D4D9BB68838ADB68898EE6E725BA89F6CED29B3C2ACA7CBB2CB739DABB20881525C61CCD672F9338C2785DA1E4663E8BD2342BA31263FDE297025D957996AEAF6EF18728797F778B70BD9B282910A5E6455BDD95B0FDA784B045DBB001B5DC1665B6F10478F08C726A2137EFC4EF39E324E6E56BCCF3F28E505DF1F368FE539E6D6FE733B9A717C7494E6A35BC3EC18571BA578966AF0212A362AF6AFC64C65779C21404EB11F9F76476BC4D88808E52B42DF3287932BBD85E27F1F2AFE8EE7DF64F941EA5DB24E191C468E232E103FE749167B7282FEF2ED10D45FD6C359F2DC4760BB9216BC6B5A9E93A4BCB674FE7B377B8F3E83A414C07381E5C95598E7E4229CAA312AD2EA2B244794A60A08A8B4AEF525FE4FFA637AC747834CD676FA32F6F50BA2E3F1ECDF19FF3D969FC05AD9A2F14835FD2180F3EDC08AB36523A79177D8AD7157E52776FA312AB43F1068F81F9EC1225559DE2637C5B0F83BDAAFC0395F7699E6D2EB3A469567FFEF03ECAD708B77E9FA96557D9365F7AA0F31E451B1017DA1DABD2A22396B05E1B8CA4E2066167A430B814FF95EAD16A8B6BA265E474E50AD3B49574DC3B5CB4A3D138462B31741DA355E3C7310AF475825B35BD91BFDFC764CC2A1D9A815C66DBD48AB3AB8A9EC66B2C08C3086E2AC843B8FEAE8C18A1D077BC506BD1CD9AE831112D8D2B223FA368F5DB36CAB1740DE808B564A4B8420035BE862F821504A3CD63353ED0F12C21D816E9ED3057DECB9A5C24D11DE14F377352B77EB427505FC5CB65197F6236E555860518A5DE26C5E23B3CDD77721D3C3B7D83BDE99D747C92C6011C25CF4E5FC579F93188FD7F8D074112DED3937A3946C9BBEDE69A0CDD817B7A8B56F1324ACED2629B47D5722A6887CE964A30E7DDCC1507E2D16675B335CF0719F22F57AB1C1545FF7E2D83A6A27FD83EBAD97C3FEF9F4CFEBD560004C0E30800FAFA29C310A5EE2CEABBC61C2C4AFF863F47C5AB2CDD1617594C16A192C698DB9EC66994540DFDFABC4405162C1B06CC196DBE775EAB58960795C7AA5F22B4457A5F9C2BEFE489F7F5C061A4440FDD15A9DA6D2E4E50895D85428B5D5D85D6F8C08DFA164BA08AB25E80EA7589E1583829A3289618F82820DE6321C358DA673D43813C1AC86006B2E2EBAAEF32E838CA5782DDAA3F981B9D5F1728FF4403E6E3865FC101D36370CB43C86604FC4C9203B64D4508D5FA97054F5AA9D780EFE3043DFA3F535B0110899CA004ADB925F8689D5F7D8C51B23ADB44EB01B66834848ED353E7E893CD6F01839A95E5112A71BB2552996221940AFDAC03B7B7D3D14630088F9662529682C9E534CB3791B892510B7D7D8C242AE29B7859F147D381B6DC7787689B2053AC23CCF0BFC8E3E508DD84B732DC666BA85D59C5D698B66E3BEC42194C62DB935A5F8BB55CCD84BC52D77B0B1C1B5DAB556FFB53AA6B29906A990890AB76DFC227F17A371A744DF474A8358DB468AAF7F6702973FA38BA14C4E32C66D52617FF86D631B9399A2A5A6F4757AFCB00B6E22A2F2EE5322376FD4333E2C0E8EF8F11288FDA0CF4D5610FD35990EFD0E7AED2C34D1F45164E641AB7382E93E1D794AFB29569072D904B39E1E5313C56B61B799BAED951392B4E9368DD1E2CEEB17157830C339230BB56284FEE307B794D1799F61691430694AABFC5983DBF46C916FF7DA0F057A87A92479F595D353E2CD47D9365842DB4F23395E1356B0DECAE03C161F84C60ED8AC1EFB2B465C4BE996BFF4049927D7695C72509BFEBC5E1C46175611D86DB32DC5D71FE745B5E67C9735776D6D5BF71D5F0BAFAB7908EEBAB7FC7AA7FED52FD7B56FDB94BF5830356FF9B2E234E170709340055D0BBD28A37D8E966E82C718F27E826C6934CB599E4A6290A08E2579FDFDC381B47DCDE556F64D05FBB08F6655164CBB862363F7935278BC5EE5EA7AB99E998B174A6004FDB5830F12D16059E338FE67F51F00700B23DEC1660B3F12442DCDFDB3B90A9E4287221B43E9360464B3A9CD09B4C29C3A285470F480427923F3D67464D7B32BA37C1BAD3D41C54E1705F68E2B99327462475C75024C2EB95B427F19AE32BC34B9C3BD962474E3AE6128C68E900BB458B7A110DEDA943485A37D865CBC38E96B833C27A42C781D10199422DA81BB6F23E7E707648670086B5F4CACE248423BC4DC96D32798E077067D34AF35F02D800E328800E6805B30090BE075775298910C20CCA28B4D86313B14016A29FC2F8E8B26E070CD43DE376986EE3D42E157B07AED35C102628FB6A0EA8C29B6C2E2C310E7687DD3937C7A70773807D0A9381326E5A88AACCF6A4FC4CA069BB63404BA86C84B8E00859C300E48F66110DFBB60ECA0B6EE286B519D00670279E77648E6643D80573D3EE706026197696B55DD53B0ADD7855C7B78FB314BB69290B2D54F1170C362205E88BEE54D52F05A2F19D8246E065D208E02B54F2333616691B5017A70A853362737AC381A63D5D5C58DA339552DA3716C8028037EF1A28826577A185E839484E6D1D9C48629E3940182BB74083F07142A5D5452D086E503860018B4A287546A91A4546B4EA016401F80E7DD641A97649A5A6DC5893A4CE32F8B92ABA0C7F799BD3128963B8720365E108A3312D1C0C88C70B913067A2E9108768D6046BAC41B94E148B51190E44639C02D12B1803886A3046E718A5EBC4015D608E03241AB930DCE033C1005E40213BA7A09DCC076AC86CACD084E90653083EEFCCC4024D00CF2984D7870562D0CEAE541D780026C0A8BC708AEB7945F614B3064C885EA13C2786F7E654E399D8D8A48BF4B9C7FA0230480AEE0D3293A8D9062A5BCC713FB7C81F87BC7D1881B1BEE126532EF3D2644A2CA3068804F63124D000093536E47BC354EA4D414197B0A0D5FEBB040207A05C7F085EA3FED650A17BB090278377EE4D83C1141B1C626E359EB637B2C71C44F40E23F6631618381CCE31038F36EB6DAA35BAE8155F944608601FBD028A83CD38E2996A33772C26170C38F6E4C7D076D79410621C64C630A46F20B2DF1083E28E6EACEFC9345D068A9971B610659720654F061A62925AC834E8E2CCC8E6A42D8B44B2B2C3457D0930FD70B8006E0B3E7C1BDDDEC6E99ABB3D987E995DD557071F7F75E57F8BEEA686B158169ACB7419B6ACA732CBA335924A71D718D3D3382F085FA2EB881C973B5E6D946A4ADC15084935DD09A15555904DA8AAA94EFEE602BC865B94F7B4ABF196A5A7984A22E68A60244FEE6ABB19B9CA394AA25C73F0FE384BB69B143ABC6F6A4DFE17DBD75F5408870B097199590B855B92E6CABC77924CB3860E2A1A6D94C0413440BB6144538754F9F6BA20AB0902BD599507413F4D46BCCD7C1154BCFAD5BC837CA186C308B84DA81060B0AF438C6208427B79250FA5FDEAA1B6E4364A416BC907F7F6DCC5923C14EEB33B2C7AB5240F877E7287C15D1CC9C3E13EBBC352AF86E441AAA59319A6C2E229E85835C4F01D06ACB1F5AE674C0802BBBC9107C23E7A6866952A2728A52679CEC8096F0BB44B2FA05E8E85770474AB4E576740DF7618CDA3D788F100E8270FDD136E5E141450287187285DC9C883948ADC610A5735F21085020F478826160A9E10FD36191D17B70E06708800D0CE7E11D87EBAEADEDCA3C70369BE7918DA2A5B5230B49A8BF44C10844BF5784042C1645471004BDBD1C88E695FFBCFEC04DBF652361E9258E20E51B8698D072814F861A881C67D7E80DE031FAD0BABD4EDB1AC0EAA6D683C590557B29A052D524A3D6CAC2E3F5630B9BA0A1E2E407D8798E001D49F3C66137A4398309BD06F0F71D870F1FCE0730100D8714A005BF71B383BB74F75CC7F201B55454FFAD8293D805D473F7724B4EA306B50499173B0FEE2D1B6DAB54CC0F9A3BE74479834EA4F1E01C2EA4A1D2136587DF1B0C0AA03F4009C1F716350133F6127A77D0324ACA1D37E08D9FED4DDEE2D9D9656F9E3A4B114884E71098358CF9E48D13DDE8E487963739CA5ABB8CAAE392BC8F549EC06102752E54DE08EEA404F147B6B036DD74F198473C31DD95EC308A60AC2A1E849690244682045100EDB7BAB83D0BA9F5268CED67794030F29988268720826A52666A2C3280B9F8BD025D06EB41ED0F92FE8B188095A1135BD62522A32AC25E1B3343A29076D1B463984BC8C8E52A86104550E21F16452CA0111DB5B39C0F495AE7B183C0CAFCD0A8D642C992A7D14A7821340792C193AD3532280F0C08AD464F774D6A2064058151273793A0A870209AE3C62EAD2A4340724B9B7DAA8994F9ED14AB1311C9174D015286DAAA328429918280F6B4CF50DE884784F2F72D3302E488089249480F5F96A93320043CD1A72DE9BEF216EAEA9F6B0B6A35AE873E676BD6AD127E18DA7ACFD8DBB36B7AFD30E090FC0E958BECE901A7202BB72B50519C2D21B920CA7650E4C6487541B35F5B19BF6A870E0BD34CBDD15B0CCA014CAA9E9169493397A402F88F7A84DF5ECB6E5EDEA4B165E2EA5215DB4872D6FA00672300D09A8F7CACF54F35A3BAA82DED7341FBC30317642FE2694AC3BAD2966208FD394F1DB6D6E51C0F4541A6BBEF0D4E6132001795AEA3496C7A2CD88EEA8583A507E67802C828353AA27A763708EF643D23325535CAEC28EAAD02FEC37CB14A759DA42FA78C513920C5EF1A2A019E372DA765D653EC30CF814AF48CAF6D51DD6CACD1EA9B077F55B729CC4E44C2CABF0364AE31B5494F5C324F3A7FB074FE7B397491C1575DE3F4D487F21DFAFE994A17EF08C64A8A3D5662137F7CF7327508A62253CB9C69D08E3D7DBE3BFCA5665E058DF5DF37C438DFCDF74907E8AF2E5C728FFD326FAF2671E92D323DFA24273282BB7A69EA52BF4E568FEAFAAE58BD9D9DF3F088D9FCCCE73AC342F66FBB37F9BB1707EC54FF728C47D9518FF74DE0AFF5DC61BF4948C35B48CAB1782E6DFFA3FCF5CE79473587B02E00E3AF9499E357497BADA7B1B49F3EBBC69D7A76F7955EB8781D87A00CDD7AD40EFADEACBEF175EC7FE9AAA3378C2538B4FF7BD61B649F661E15679F716DBEC04884BBD0F6B38682E7E80F983CBC60F004D4DC4F706EA3CC4E077A6EEEB38B3EAF2737F5D66D9F1CE609DB4A6222F28487F23E3E786A85B4DF7564F6832AFD66D7061B598B0DE198C94A4AE159B0B1C2135BD33364D567A77676A97EE4C7B80CCAFEFA65D9FBE5928CF73F150371BCC79D21E22FADD8ED926F7BEBB075667DE774640C8B70FE12A08A7DD3A68BDB7FE014CEDB27E600D07D0FE0734510DE1D088F70184852D5C0D102238D35E0E1000DA801E129451FFA879DA505B9344DFDDD7D0E6E477074713F203285993953F717D85B6ECEEADC28EE584DDB308B269FBECDECA7A8888D0A4A5A864DA3F8A8E175D9DC71FC0E2D6E9FC214CF7BD705A80E40137E5824FEBA8750DF915367D1CC6AE3BE9D570AB9B3E5203CE7DBB09CD7076DB59C276990D1508DAA123106671D033E46E3B7DA2B6B0E5655B47DFE0B3A2934CC7D8AC0CE44EF733A8C653AFC1ED2ADF9BFF68695BDF7F2BABBE3A0CBFE8A9BCA4E6F026B6E501612E715D7C18D7F66E3556059413572C4A8EB3B428F328566FF7C14BD27419DF468988B554CDD183243C6400E59213748B52E21A0A74B97464BC5C87819574DC46BDC723D3E063A6F2A3618294E89709CB1A7A0A6487A236E58F8D266978266E25A55C4EA37B2E7AC2B2375F70BF430DB067D48CA507DCBD30238CFA3661437D2FFC018C7EE0A2E5695A00EED2979EA3759292771F8CA34ADE74A9CD0892777AA1B8BBEC2C9A20BDD0AB388DAC64388D7016544FADB05DFB0F688621036C74EDD02EE4C2BBFEBB558AF19602FE1A614CB01C411D9CAE4B91DEF51404A83311074A06D3794A76E34B3423715E12003F8E8A65B452D7CD2477C7D4B74623F9CF83E8CF58F604BEE7C63336BD13C5D125727531250F4C79C6333E9EEAB36BCB637A09DED7E04CD23B1DCB6CF8F93CBBB51A0E77F4F8AF4A07B218ED1E9482C14358CD82572B4D7135EB708BD2449C94DD68CD58B6C64F6976ECA1E8EE9D0255A63DDB28280EF7D96BCA71345DA1C46F7A28691825705300DB4D02E3AA81D3DD4E7D946228DF55BBB7AE47C910B7BF97CAE674A5D7BD543ECB24D66163E677A17E236FF874D7BF496C00F9DC5DA65D2D17C0725909D60DBE6E2F8CCBF762F01010FCA4DA804B79E0963AD0DD32DF28B74385BB37E1A15DAAD92E82453E1AB6EB989172B7DF9051A30E36F09EADE67CADDA64D673F26D7D63ADEA76A812E33ADDBE8A312D575B77E7DE682A22E622E961D6450F41516CAF9F4E41575E8BB7F135C995F2CD798A8AD0EB19F910E47CD61EAD15E33AF5BD7B47F3D57586855E9FCDAD8A94C78B15C8F4C08D02B979AD4903B92A52DF45564037DE8A029B3DCDA201CE2C9905B8B0D6537A10DFADD374C32F88DD78544FE27A3ED1AB7E215E91625776B1D30800D75839CC3C56C5D22140114C8C1B1DBCF552817385DA2EB8E7DC5DF067EAA22583959ABB12AA3A5357DB510385750573D7F5A3D0B64E4976A2DA53F55507BE7ABED89D7B46E6C19AE0CA316EFB416FBF608563C53E72315B06A09E594A5ED642B35E3329A7239B216E8BF7C1EA522B665C15DE6869532FB4EB624661FB49993995439D5C23FA455E118BA83B9345E73C882AF02D0F717384438F7ED92149822A4284595E95D0456E398485EFBB23953B0E0E110A9D180F2345DE10498F3F85249132CD4422F87CAE9F244626113AE0AB21D5E92C701FF435C64A729F342FFE85664163116DF4C32F7EF4B2B6E3910D9DD6D450EE74B0531333E3B0B7CA5BF55FE4F7FA06209906925D48D6DF2DDF5BDE63902DBF9767B2629611ED2FD7E14D98FE1C98864AA747DD7A4E4A8A772C3D3D179C5C58859D5E8BEB25DBE189D5BFF7A619AED6F33960A08E2798FB6C23DBAE17FDC885CE979889777C712C182B8CCB34C3336AC3B2CA60D6BD1F3D0BE88BEF9059E69D7B60FE73DDEB87B735B4339AE2BCD8364FF550827A05865D660FE60CEE23EC822590CF60DE3F0DE60D3A6B513F1B22EF029A6D8771CF30A4791D9D0DBA17A6CCACB0ED8F0D32DBF0C15ADDAB57EE4C511E586265878B3A86473FE09FCA434A878BCB6D4A6E64AB7F9DA0225EB720C80351295A0A5B48ACCE597A93351B5A12464D15E5A983325A4565F4322FE39B6859E2E2252A8A385DCF67BF46C91691571AAED1EA2C3DDF96B7DB12938C36D7C91DCF0CB22366EAFF70A1E07C787E4B7E152148C068C6E412BBF3F4D5364E560CEF53CDC5330008B2D546AFD723B22CC9357BEB3B06E95D963A02A2EC633B84EFD1E636212A7C9E5E45E4FA387FDC7E29D01BB48E967717F43D2C18885D1022DB0F4FE2689D479B82C268DBE39F5887579B2F3FFC1F20F2C1FCE2020100 , N'6.1.3-40302')
