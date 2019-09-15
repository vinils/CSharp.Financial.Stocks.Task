select 'INSERT INTO [DATA] (GroupId, CollectionDate, DecimalValue, StringValue, Discriminator) VALUES (''' +
Convert(nvarchar(100), GroupId) + ''', ''' + 
format(CollectionDate,'yyyy-MM-dd HH:mm:ss') + ''', ' + 
isnull('''' + Convert(nvarchar(100), DecimalValue) + '''', 'NULL') + ', '
+ ISNULL('''' + StringValue + '''', 'NULL') + ', '''
+ Discriminator + ''')'
from [Data]
where GroupId in (
'63DBE169-40F3-49E8-9DA7-8A4A192BEDB3',
'3B2DCB01-B756-477F-B9D6-5137E38DE2A1',
'A5403FF1-8C96-4F49-978D-84CC9FBC2824',
'ACB61B44-F3F1-4A82-83CD-7F5F833352A6',
'B24E5530-FFB3-476F-82D8-EB223D8D3432',
'86FD55A1-D887-462E-9AF9-B075858D68A7',
'3040729D-3A48-451B-839B-A3B3CA93FAC0',
'46AF124B-DE0F-4580-83CA-15DCBDB6AE67',
'C800A77C-ADAB-4FCA-85E6-206B4A4DC165',
'792FCFE8-B8CC-491B-B305-4BF7324297BE',
'7B677ACB-8096-48B8-B8C4-F3ED39525484',
'774DC359-A4BA-48BA-9207-553DBF3D4282',
'7D7ADEA6-8E2D-459C-8A9D-FCF2D4333BFB',
'AA080EA2-E11A-4AF0-BEF4-9B80C2DC35CC',
'4526C8CA-6161-4134-A79B-315C64A938A5',
'ABC2FDF1-208B-4E7C-BA68-182BCBB66D01',
'AA9E621C-1D24-41F9-901C-53E25F2BDE0A',
'CA4D3219-30B5-475E-AE98-7B17F012F667',
'D1612130-201B-402D-B710-E31EC59DDE70',
'BE0727A9-4BB2-4B70-B4CC-C9CD9D5B01EA',
'AF77147A-259E-4F43-88CF-CEFAB2CB932C',
'1760FF81-157A-4CFC-91CC-01C15D8CE505',
'2A048DA1-0AD8-4127-8C7D-50A34F1A7A85',
'A47420CF-F641-4445-92FC-BF44620D91BF',
'E98F2333-0603-4F1E-8D46-010E499261E6',
'A77C2296-1068-4416-916A-16CF4BF75226',
'31937AA1-6F13-4F06-A766-F8409427E0AF',
'D20F97CA-C705-4B7B-B837-6EB9F8FAA61D',
'33DD6AED-2185-4D94-BC91-C557FD6F237E',
'89766F83-A121-4DBB-9E0D-37D030A4F47B',
'4B14B259-12B6-4247-BDD6-B2B67EF27EBB',
'F33FA868-1ED2-4463-93BE-6EA00B49E3B0',
'5418475D-F791-4F09-AD5E-B6E26794E564',
'070A8739-3E42-48C1-8874-ECA4DA9F5898',
'9043967E-E06E-4838-A468-FA5378204CE0',
'9111CED5-9F56-4BFE-893F-DB1A64F7A8D4',
'2881B350-AA09-42B5-8874-835D523557D8',
'F7469FF3-5360-4CAA-8307-7D1D2FC58A68',
'B0CF7EE0-14E1-40F7-9153-97FF3B194B28',
'9F2A2C0A-10C1-42D9-8214-8157621A62A2',
'9EBD41E8-502D-4533-83AD-4EC50AB9DB4E',
'E847C7F6-CE52-4508-8E72-CD295027B077',
'8F6DA4B6-47FD-48D7-8EBF-3ECD033FB2A0',
'8186B8F3-73FE-45D2-9ABD-4FF6CD5F6349',
'DEA6BC7F-9CAB-496A-B1FB-31BD456AD919',
'5F955767-9321-4E9E-900C-82F23F1D1320',
'94BE8BE1-F8E8-4C98-ACF4-152839910BBA',
'FD56F009-788D-4EB3-BC33-5B817C028085',
'CDE21285-04B6-4EA7-9756-18F745A268A7',
'20B02500-169B-4CA9-A943-CECC8E33B871',
'A060E9AC-3642-4181-9886-1E5FAA827544',
'C4362962-08CB-4400-840C-86C9933C1D43',
'1CC31AD9-1173-40E6-AE76-C7688583514E',
'A7F8031D-55BE-4A97-AF3A-2BA404C50314',
'FF993AFA-E2B5-4537-8D2F-EAB4BF58A0A9',
'DA28C09D-C5BA-4582-93C4-0F956CD46112',
'1683CE8F-53E1-4126-AF89-68B65310A8FA',
'78577DE5-0685-42D8-91BF-D78991893DB3',
'4675599C-89B4-4A12-A189-4B050378F04E',
'BE3CC30B-57DB-49F8-A477-397FC80326D2',
'894694B2-ACA0-4103-8B71-D7E78CFDCE8A',
'D5C884EB-501E-42D7-868F-679D449F6A84',
'D3606A06-7B0C-4EB0-8619-0453442B210B',
'14957854-0D90-4A5C-83D1-05DDF0E63BDE',
'862C4955-FB9F-4916-BD29-070CCB3A9797',
'21098039-02DB-400A-B108-071F4FEA9444',
'EBB670FD-494E-46AF-B05E-0BFDD2CC474B',
'BA27B26F-543B-4E4F-BE32-129F6060FABA',
'6B30D533-0173-4248-9E30-17A12C94D478',
'F755F1DF-EA01-4E65-A785-1CFC3181EC16',
'A03F1CDE-804B-4917-86CB-1D784E0328DD',
'D478A7E2-BF55-4890-A7B9-1EF1AB75A04F',
'C0653B14-11CE-4A26-B078-1F1CCA3F4010',
'3B17281E-23DD-44E4-AB96-2135D423DBF7',
'08534EC4-B5A7-432C-AFB2-21C3E958A1FE',
'29DFB33A-4CFB-4762-9F3F-23BA18C3A021',
'6CBC1E72-D0F7-40AD-AE4C-241054C5AD96',
'BEA704F4-8EED-474C-BAAA-2716F82A0E05',
'E7DA8A03-D407-4C5E-AE88-2978722949D4',
'BBBA5CC9-2CF5-4C61-896C-2A11AAE8D89C',
'53EDC6D6-45C9-49DA-90DB-2A2AA5A8EEA2',
'E5EB652E-19C5-42B7-B364-2A8C284C597F',
'94418A3A-1AD8-4FA0-8304-2A9FC6D86CB6',
'C2708B9C-F8EB-4855-B22C-33554FDBEF1C',
'6D666407-58F4-434B-B885-3385E46987A0',
'E45BF7F4-8B37-4900-B060-33CE1995B35A',
'0E12D86A-CE0A-405D-830B-3433F8FBC98B',
'97498E88-B521-4C79-8E6C-34A182530525',
'1F020963-9048-44C6-8EC3-3536AD103516',
'B8AC020C-48BA-4B28-BCB5-35B4200954DE',
'F9D424B5-6F31-4B4D-9D94-36FFAD566A24',
'2FABC64C-C5A4-4FEF-A289-38926BD56452',
'B58A764B-DD5A-43F1-8185-3C001BB46130',
'0C7443CF-71E9-446B-89CB-3F11763D0215',
'3FA7E846-E42C-45B9-BCAF-40B8B392CA0F',
'60860E6E-DF5D-4BC8-8E6F-46DD4B6BCE32',
'55650B60-A141-41EA-A7B5-475367500073',
'FB474317-F26D-4A33-9A03-49F4EB163941',
'C1DF9CC0-090B-4C3E-9160-4B6ACEC1796C',
'993536DC-85B3-4F0C-B5F7-4CB42BFC803F',
'A3339C5B-B4C1-40DE-A78A-4D3C85A36FFA',
'1CA620E3-7BC1-43A4-B397-4F92FF986D11',
'37A2B928-0C43-414B-9B58-4FA1A94197D7',
'474A22DA-FA34-48F9-958B-57A71E9E4D65',
'048F3C42-A5F2-4E2A-B045-58721383C711',
'1C1F2917-C49E-488A-9DAC-59FD140503BB',
'045D902E-A449-4419-BB8B-5BD26C7E9A86',
'49C4542C-9718-4847-A030-5C0CD3481F72',
'329B212A-2E21-4BB4-908F-5C6263EE1372',
'985E804B-4194-4BF4-BCBB-5DB94B735FFE',
'D0604664-0817-419C-A582-613CE2ED0C09',
'0C2CFD07-D327-4A39-8CD5-65A3A48F265D',
'596C415A-317A-4D53-B89B-680D9BD172B3',
'824C2C6B-9AA4-4C81-A058-68D6304BF081',
'21AD8094-5A26-4339-8CAF-6EA47ACBE15C',
'98347DD1-9A9D-48C5-B12C-6F0B727284D4',
'FC5B02EC-404A-4FFE-98A7-6F3FF88701BE',
'E5C407E3-4095-40DD-9671-704F6E701DDE',
'0AB1EDDD-8A22-44FD-B48A-73F47AC7DDE8',
'57F9278E-90BB-44B3-AE97-7A55D9290241',
'29B13167-7BCB-4B54-87CE-7B66F12B19E5',
'55B9F477-144B-496C-8C36-7CC545AA82F9',
'5FC1504E-B8B5-4295-90BC-7DD194C55268',
'5B314B59-3410-4758-8465-7F8C6A0EAD94',
'4F07C13A-76DF-4AB8-80F3-7F8FFF0E8A65',
'F5EACF98-D690-4E54-97BB-83267599DE78',
'A63B5952-1555-4A78-8DD6-8563A6A37CD4',
'F5C9D25F-9ED2-4BEB-8D87-8D81BC2E6BF4',
'7303E197-F5C0-4A06-B3A4-8E4CBC10BB89',
'9A3EB48A-738D-4B94-A00F-8F4ABEB9FB01',
'23C291DE-08F1-4822-8854-8F6372A14720',
'955F1405-B21C-445A-A4F3-9080169FD341',
'C05D4DCF-AE83-4F47-B701-9117DCC72A8A',
'EC559343-09EB-4D84-8D6E-93828E44060E',
'5EB12C92-FBB7-4A98-8B68-93A2EED41D77',
'EC93DF75-298F-43D2-A699-94C67491C4D4',
'073CD938-E8F5-4FC0-96A3-97FFAD6394F8',
'A4B18D31-781F-41EC-8E1B-981FC38008BE',
'5C937840-07E0-4E9F-A326-987F477F67A2',
'5B7B459D-029E-42AF-AB82-9A0AD4F3F6BC',
'4C5E9DE3-8BA2-47A8-A7C8-9B988C4E0518',
'295EE2E3-66E1-44EF-8C2E-9CC86684433E',
'6A920AE4-3B19-4C31-A2C4-9D1E1CB3623C',
'0F8A98A6-3765-4FA9-A2E4-9E27E6FD0049',
'F1B9E6C4-6EF5-41FC-A652-9EE6A93CC047',
'16EA1079-8826-44A8-AD1D-A2FFE57F0EF1',
'A9C60962-395D-4F7E-AAB6-A3CEDB9C5464',
'10E81BC4-0785-4FDF-8B53-A455A5ACDECF',
'DE0B7D03-7778-4D63-BF30-A5C7E2560820',
'3582E97B-C529-4F64-9037-A6628C37B7F1',
'1361636F-B9F3-4A0C-978C-A8081E0BA2A5',
'1B8F0D9B-49FB-4795-A6D9-A92DD801421A',
'0A461C86-BE11-4677-9BC5-AA7B414A0E18',
'1F7DB739-422B-4452-899E-AAA1FCE31923',
'BF1F6BD1-2D13-48F6-9E4D-ADA01F75B2D8',
'0AE5AB59-210A-44E0-A4C8-ADBEF4B8EC6C',
'F094D08C-9261-4056-B6E3-AE2E8F8A21E4',
'4CCE6B2B-6FEF-4401-98D6-B004382E22B9',
'4AAD28A1-7815-48D8-ACD9-B135B8692B60',
'A4BE4C59-D9D4-4894-BA2D-B286BD6F79D6',
'94A502C9-9D04-46C0-AB29-B426F4FF9E46',
'7BB8022C-FA8A-4664-BCF2-B47C49C1A3BA',
'E47FF210-4E70-4ACF-B193-B579BD11BAD6',
'A93914B4-DBA1-4809-9DFD-B7008DFF4C29',
'51BF9848-DE14-4181-B92B-B7546156E0BA',
'37614B06-D82C-4BEA-9D03-B91C8C6C60AD',
'17BECC8B-18DF-42D7-87E0-B97C1238EBAE',
'8C52B8D3-9560-4E07-9C0E-BB4D6F3E1378',
'5B886E80-ADD1-4D30-89DA-BC688EFD60E2',
'B4A3AC03-1D3A-41C1-9EB8-BC95612C541D',
'FEF6DC13-7EF8-44B4-B606-BD4185D374AE',
'BDE94705-6CB5-4D7A-AEC2-C0CF292EE3C4',
'9F0D569B-55BA-47E5-992C-C103C3528D23',
'0B5522BC-DBB9-4F82-8F41-C1071C2573E0',
'4CC848A4-9ED4-4A83-B8BD-C26D2818F275',
'FD2DDED9-1D8C-4E18-86E4-C4AC38F4487B',
'4BAF68C4-01D8-4C14-9809-C51CA2C81811',
'F96C05A2-88F0-4AEA-8DB3-C5736C4AECC2',
'C5D96839-B4BD-4D64-ACCA-C9C901067F5C',
'7DCD7E9A-23DE-4CA5-AE22-CB8A10C63274',
'443D5D34-ED9B-4B67-803B-CC75F25704B5',
'AF7FF377-056B-47F2-AB99-CFFBCC91C2C9',
'44201750-9216-4221-83A8-D1305CB686DB',
'C6813217-49DD-46C0-BDD8-D1393ECE8B8A',
'AA78C081-034A-4782-A9ED-D21851A57E3B',
'08B6406F-2261-44A7-B400-D263DB4CDA97',
'8D9BA599-C933-4C85-9AA2-D446CD58BFAD',
'E65F1C2F-CA19-4D15-8516-D6D582F99660',
'B490BB70-742E-4B4B-ADDF-D7FC9D82E2F3',
'ED746FB6-0FB5-4C4F-81CC-D8E380D384A9',
'DF210F91-7427-4C3D-AEEF-D9150DAFADF4',
'3C5B2E4F-8B11-4C82-AD8B-D9DA3AB67DC7',
'2EEF60C6-AEE5-4791-9775-DAA67E739130',
'7F302ED1-E6B0-4254-9EFF-DBE3FA6DF324',
'E3D106F0-C9E0-47E8-864A-DC18CA6E7D9F',
'30D107EA-1A99-46E6-B8ED-DCAB47B9BF37',
'5A21CD74-9D05-4716-93A2-DFD3FA3046B7',
'A57FA201-D0FB-4436-B5FC-E117EEB9FD06',
'6D7FA177-16E8-4371-9122-E2079A25FBD5',
'88BC37C1-8DCF-4289-A1E2-E2E292A264A5',
'CB19D2D0-D733-46D7-9157-E47F6E55351B',
'E899D5AB-B8E5-4A05-AB48-E8A80E5AB60A',
'BEAB4A75-4C2D-49DD-AEFD-E999D93F6600',
'96BBD7D2-58CA-433B-B980-EC2EA9C59BC0',
'773C0B97-827B-4FC5-80EB-ECA1EEC2C911',
'4BE47701-D50D-4106-A9AA-F1B3D451558A',
'73D612CD-490B-45A5-8F19-F1BC45868DB1',
'ADBC8091-B10B-45C6-AC93-F24EAB114768',
'180D98EE-2A55-4745-9326-F336CE4770E3',
'0694F5CB-7FD3-48C7-8EA2-F429648F24F4',
'D370CCA8-A8A6-4944-96C0-F4AB328554DD',
'18CC4EFD-86B9-453B-9369-F75282BE9C95',
'E09C5895-A70C-44AE-A460-F761965E830D',
'9F7C8AC5-1995-4E3F-A9C0-F92F56DAD85E',
'E0D46D4F-ACC4-4541-B9FE-FA97FEE6AE11',
'05EECA3A-FA7D-4858-B2F1-FAF9B41B903E',
'DF01090E-21C8-4185-8E7C-FB23FEC7BE09',
'CC86C0BF-158C-4109-95CF-FCA1B5E6EA5A',
'72ED077C-D3AC-48DD-95BC-FCA4462358E1',
'43D46DC0-92C3-40D4-8C89-FF466C081AC1'
)
ORDER BY GroupId, CollectionDate