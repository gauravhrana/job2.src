IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RenumberRunInsert')
BEGIN
	PRINT 'Dropping Procedure RenumberRunInsert'
	DROP Procedure RenumberRunInsert
END
GO

PRINT 'Creating Procedure RenumberRunInsert'
GO
/*********************************************************************************************
**		File: 
**		Name:RenumberRunInsert
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
CREATE Procedure dbo.RenumberRunInsert
(
		@RenumberRunId			INT			= NULL 	OUTPUT			
	,	@ParentSP				VARCHAR(50)												
	,	@AuditId				INT			= NULL					
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'RenumberRun'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @RenumberRunId OUTPUT, @AuditId
		
	INSERT INTO dbo.RenumberRun 
	( 
			RenumberRunId							
		,	ParentSP							
		,	EntityName					
									
	)
	VALUES 
	(  
			@RenumberRunId							
		,	@ParentSP							
		,	@SystemEntityType						
	)
	
	

	
END	
GO

 