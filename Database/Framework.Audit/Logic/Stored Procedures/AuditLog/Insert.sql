IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditLogInsert')
BEGIN
	PRINT 'Dropping Procedure AuditLogInsert'
	DROP  Procedure AuditLogInsert
END
GO

PRINT 'Creating Procedure AuditLogInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:AuditLogInsert
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**	
**********************************************************************************************/

CREATE Procedure dbo.AuditLogInsert
(
		@AuditLogId       INT			 = NULL 	OUTPUT	
	,	@EntryDate        DATETIME		 = NULL				
	,	@DataMessage      VARCHAR (1000)					
	,	@ApplicationId    INT					
	,	@ConnectionString VARCHAR (1000)		
	,	@SourceComputer   VARCHAR (50) 
)
AS
BEGIN
	IF @AuditLogId IS NULL
		BEGIN
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'AuditLog', @AuditLogId OUTPUT
		END
	
	--if blank, then assume search on all possiblities ('%')
	--IF LEN(LTRIM(RTRIM(@AuditLogId))) = 0 
	--BEGIN
	--	SET	@AuditLogId = 'NULL'
	--END

	INSERT INTO AuditLog 
	( 
			AuditLogId       ,			
			EntryDate        ,
			DataMessage      ,
			ApplicationId    ,				
			ConnectionString ,
			SourceComputer		
	)						 
	VALUES   
	(  
			@AuditLogId       ,		
			@EntryDate        ,
			@DataMessage      ,
			@ApplicationId    ,		
			@ConnectionString ,
			@SourceComputer	
	)
END
GO
