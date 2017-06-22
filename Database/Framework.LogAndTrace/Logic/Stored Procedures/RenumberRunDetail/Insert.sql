IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RenumberRunDetailInsert')
BEGIN
	PRINT 'Dropping Procedure RenumberRunDetailInsert'
	DROP Procedure RenumberRunDetailInsert
END
GO

PRINT 'Creating Procedure RenumberRunDetailInsert'
GO
/*********************************************************************************************
**		File: 
**		Name:RenumberRunDetailInsert
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
CREATE Procedure dbo.RenumberRunDetailInsert
(
		@RenumberRunDetailId	INT			= NULL 	OUTPUT
	,	@RenumberRunId			INT			= NULL 	OUTPUT			
	,	@EntityName				VARCHAR(50)	= 'RenumberRunDetail'
	,	@FKEntityId				VARCHAR(50)
	,	@OldId					INT
	,	@NewId					INT	
	,	@AuditId				INT			= NULL				
	
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @EntityName, @RenumberRunDetailId OUTPUT, @AuditId
		
	IF (@RenumberRunDetailId IS NOT NULL)
	BEGIN
	INSERT INTO dbo.RenumberRunDetail 
	( 
			RenumberRunDetailId
		,	RenumberRunId														
		,	EntityName					
		,	FKEntityId
		,	OldId
		,	NewId
									
	)
	VALUES 
	(  
			@RenumberRunDetailId							
		,	@RenumberRunId							
		,	@EntityName
		,	@FKEntityId
		,	@OldId
		,	@NewId						
	)
	END
	
END	
GO

 