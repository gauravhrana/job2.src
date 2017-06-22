IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogRawInsert')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawInsert'
	DROP  Procedure StoredProcedureLogRawInsert
END
GO

PRINT 'Creating Procedure StoredProcedureLogRawInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:StoredProcedureLogRawInsert
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

CREATE Procedure dbo.StoredProcedureLogRawInsert
(
		@StoredProcedureLogRawId		INT					OUTPUT		
	,	@StoredProcedureLogId			INT			 				
	,	@InputParameters				VARCHAR(500)	= 'N/A'				
	,	@InputValues					VARCHAR(5000)	= 'N/A'				
	,	@SystemEntityType				VARCHAR(50)		= 'StoredProcedureLogRaw'
)
AS
BEGIN
    
	SET @InputValues = ISNULL(@InputValues, 'N/A')	
	
	INSERT INTO dbo.StoredProcedureLogRaw 
	( 
			StoredProcedureLogId			
		,	InputParameters					
		,	InputValues				
			
	)
	VALUES 
	(  
			@StoredProcedureLogId				
		,	@InputParameters			
		,	@InputValues			
	)
	

END	
GO

 