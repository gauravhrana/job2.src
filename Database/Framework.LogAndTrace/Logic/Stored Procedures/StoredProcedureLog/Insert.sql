IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogInsert')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogInsert'
	DROP Procedure StoredProcedureLogInsert
END
GO

PRINT 'Creating Procedure StoredProcedureLogInsert'
GO
/*********************************************************************************************
**		File: 
**		Name:StoredProcedureLogInsert
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
**********************************************************************************************/
CREATE Procedure dbo.StoredProcedureLogInsert
(
		@StoredProcedureLogId	INT				= NULL OUTPUT			
	,	@Name					VARCHAR(50)		
	,	@InputParameters		VARCHAR(500)
	,   @InputValues			VARCHAR(5000)					
	,	@ExecutedBy				VARCHAR(50)		= 'System'				
	,	@TimeOfExecution		DATETIME		= NULL					
	,	@AuditId				INT				= NULL					
	,	@SystemEntityType		VARCHAR(50)		= 'StoredProcedureLog'
)
AS
BEGIN

	SET @TimeOfExecution = ISNULL(@TimeOfExecution, GetDate())	
	
	INSERT INTO dbo.StoredProcedureLog 
	( 
			Name							
		,	TimeOfExecution					
		,	ExecutedBy								
	)
	VALUES 
	(  
			@Name							
		,	@TimeOfExecution				
		,	@ExecutedBy						
	)
   
    SET @StoredProcedureLogId = @@IDENTITY
	BEGIN
	
	EXEC dbo.StoredProcedureLogRawInsert NULL, @StoredProcedureLogId, @InputParameters, @InputValues
	END
	
	
END	
GO

 